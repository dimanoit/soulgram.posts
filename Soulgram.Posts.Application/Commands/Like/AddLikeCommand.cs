using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nest;
using Soulgram.Posts.Application.Services;
using Soulgram.Posts.Domain;
using IRequest = MediatR.IRequest;

namespace Soulgram.Posts.Application.Commands.Like;

public record AddLikeCommand(string UserId, string PostId) : IRequest;

internal class AddLikeCommandHandler : IRequestHandler<AddLikeCommand>
{
    private readonly IElasticClient _elasticClient;
    private readonly ICurrentDateProvider _dateProvider;

    public AddLikeCommandHandler(
        IElasticClient elasticClient,
        ICurrentDateProvider dateProvider)
    {
        _elasticClient = elasticClient;
        _dateProvider = dateProvider;
    }

    public async Task<Unit> Handle(AddLikeCommand request, CancellationToken cancellationToken)
    {
        var like = new UserInteraction
        {
            CreateDate = _dateProvider.Now,
            UserId = request.UserId
        };

        var updateResult = await _elasticClient.UpdateAsync<Domain.Post>(
            request.PostId,
            u => u.Script(s => s
                .Source("ctx._source.likes.add(params.like)")
                .Params(parameters => parameters.Add("like", like))),
            cancellationToken);

        if (updateResult.IsValid)
        {
            return Unit.Value;
        }

        throw new NotImplementedException();
    }
}