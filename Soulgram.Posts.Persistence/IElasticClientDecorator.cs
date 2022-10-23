using System.Threading;
using System.Threading.Tasks;
using Nest;

namespace Soulgram.Posts.Persistence;

public interface IElasticClientDecorator
{
    public IElasticClient Client { get; }

    Task AddNestedCollectionItem(
        string postId,
        object data,
        NestedPostCollection postCollection,
        CancellationToken cancellationToken);

    Task DeleteNestedCollectionItem(
        string postId,
        string id,
        NestedPostCollection postCollection,
        CancellationToken cancellationToken);
}