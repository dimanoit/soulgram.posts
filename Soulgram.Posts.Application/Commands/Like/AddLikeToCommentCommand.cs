using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Soulgram.Posts.Application.Models.Requests;
using Soulgram.Posts.Application.Services;
using Soulgram.Posts.Domain;
using Soulgram.Posts.Persistence;

namespace Soulgram.Posts.Application.Commands.Like;

public record AddLikeToCommentCommand(LikeCommentRequest Request) : IRequest;

internal class AddLikeToCommentCommandHandler : IRequestHandler<AddLikeToCommentCommand>
{
    private readonly ICurrentDateProvider _dateProvider;
    private readonly IElasticClientDecorator _elasticClientDecorator;

    public AddLikeToCommentCommandHandler(IElasticClientDecorator elasticClientDecorator,
        ICurrentDateProvider dateProvider)
    {
        _elasticClientDecorator = elasticClientDecorator;
        _dateProvider = dateProvider;
    }

    public async Task<Unit> Handle(AddLikeToCommentCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var like = new UserInteraction
        {
            CreateDate = _dateProvider.Now,
            UserId = request.UserId
        };

        var updateScript = @"for(int i = ctx._source.comments.length - 1; i >= 0; i--) {
                    if(ctx._source.comments[i].id == params.id) {
                        ctx._source.comments[i].likes.add(params.like);
                    }
                }";


        var updateResult = await _elasticClientDecorator.Client.UpdateAsync<Domain.Post>(
            request.PostId,
            u => u
                .Script(s => s
                    .Source(updateScript)
                    .Params(parameters => parameters
                        .Add("id", request.CommentId)
                        .Add("like", like))),
            cancellationToken);

        if (updateResult.IsValid) return Unit.Value;

        throw new NotImplementedException();
    }
}