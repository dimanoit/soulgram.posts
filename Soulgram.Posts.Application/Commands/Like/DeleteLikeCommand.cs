using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nest;
using IRequest = MediatR.IRequest;

namespace Soulgram.Posts.Application.Commands.Like;

public record DeleteLikeCommand(string UserId, string PostId) : IRequest;

internal class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand>
{
    private readonly IElasticClient _elasticClient;
    public DeleteLikeCommandHandler(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task<Unit> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
    {
        var deletionScript = @"for(int i = ctx._source.likes.length - 1; i >= 0; i--) {
                    if(ctx._source.likes[i].user_id == params.userId) {
                        ctx._source.likes.remove(i);
                    }
                }";
        
        var updateResult = await _elasticClient.UpdateAsync<Domain.Post>(
            request.PostId,
            u => u
                .Script(s => s
                .Source(deletionScript)
                .Params(parameters => parameters.Add("userId", request.UserId))),
            cancellationToken);

        if (updateResult.IsValid)
        {
            return Unit.Value;
        }

        throw new NotImplementedException();
    }
}