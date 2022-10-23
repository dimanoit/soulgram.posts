using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Soulgram.Posts.Persistence;

namespace Soulgram.Posts.Application.Commands.Like;

public record DeleteLikeCommand(string UserId, string PostId) : IRequest;

internal class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand>
{
    private readonly IElasticClientDecorator _elasticClientDecorator;

    public DeleteLikeCommandHandler(IElasticClientDecorator elasticClientDecorator)
    {
        _elasticClientDecorator = elasticClientDecorator;
    }

    public async Task<Unit> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
    {
        await _elasticClientDecorator.DeleteNestedCollectionItem(request.PostId,
            request.UserId, NestedPostCollection.Likes, cancellationToken);

        return Unit.Value;
    }
}