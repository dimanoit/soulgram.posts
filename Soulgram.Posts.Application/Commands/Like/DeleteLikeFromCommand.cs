using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Soulgram.Posts.Application.Models.Requests;
using Soulgram.Posts.Persistence;

namespace Soulgram.Posts.Application.Commands.Like;

public record DeleteLikeFromCommand(LikeCommentRequest Request) : IRequest;

internal class DeleteLikeFromCommandHandler : IRequestHandler<DeleteLikeFromCommand>
{
    private readonly IElasticClientDecorator _elasticClientDecorator;

    public DeleteLikeFromCommandHandler(IElasticClientDecorator elasticClientDecorator)
    {
        _elasticClientDecorator = elasticClientDecorator;
    }

    public async Task<Unit> Handle(DeleteLikeFromCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var updateScript = @"for(int i = ctx._source.comments.length - 1; i >= 0; i--) {
                    if(ctx._source.comments[i].id == params.id) {
                        for(int j = ctx._source.comments[i].likes.length - 1; j >= 0; j--) {
                            if(ctx._source.comments[i].likes[j].user_id == params.userId) { 
                                ctx._source.comments[i].likes.remove(j)
                            }
                        }
                    }
                }";

        var updateResult = await _elasticClientDecorator.Client.UpdateAsync<Domain.Post>(
            request.PostId,
            u => u
                .Script(s => s
                    .Source(updateScript)
                    .Params(parameters => parameters
                        .Add("id", request.CommentId)
                        .Add("userId", request.UserId))),
            cancellationToken);

        if (updateResult.IsValid) return Unit.Value;

        throw new NotImplementedException();
    }
}