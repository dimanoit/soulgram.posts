using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Soulgram.Posts.Application.Services;
using Soulgram.Posts.Domain;
using Soulgram.Posts.Persistence;

namespace Soulgram.Posts.Application.Commands.Like;

public record AddLikeCommand(string UserId, string PostId) : IRequest;

internal class AddLikeCommandHandler : IRequestHandler<AddLikeCommand>
{
    private readonly ICurrentDateProvider _dateProvider;
    private readonly IElasticClientDecorator _elasticClientDecorator;

    public AddLikeCommandHandler(
        ICurrentDateProvider dateProvider,
        IElasticClientDecorator elasticClientDecorator)
    {
        _dateProvider = dateProvider;
        _elasticClientDecorator = elasticClientDecorator;
    }

    public async Task<Unit> Handle(AddLikeCommand request, CancellationToken cancellationToken)
    {
        var like = new UserInteraction
        {
            CreateDate = _dateProvider.Now,
            UserId = request.UserId
        };

        await _elasticClientDecorator.AddNestedCollectionItem(
            request.PostId,
            like,
            NestedPostCollection.Likes,
            cancellationToken);

        return Unit.Value;
    }
}