using System;
using System.Threading;
using System.Threading.Tasks;
using Nest;
using Soulgram.Posts.Domain;

namespace Soulgram.Posts.Persistence;

internal class ElasticClientDecorator : IElasticClientDecorator
{
    public ElasticClientDecorator(IElasticClient elasticClient)
    {
        Client = elasticClient;
    }

    public IElasticClient Client { get; }

    public async Task AddNestedCollectionItem(
        string postId,
        object data,
        NestedPostCollection postCollection,
        CancellationToken cancellationToken)
    {
        var collectionName = postCollection.ToDbName();
        var updateResult = await Client.UpdateAsync<Post>(
            postId,
            u => u.Script(s => s
                .Source($"ctx._source.{collectionName}.add(params.{collectionName})")
                .Params(parameters => parameters.Add(collectionName, data))),
            cancellationToken);

        if (!updateResult.IsValid) throw new NotImplementedException();
    }

    public async Task DeleteNestedCollectionItem(
        string postId,
        string id,
        NestedPostCollection postCollection,
        CancellationToken cancellationToken)
    {
        var itemName = postCollection.ToDbName();
        var paramName = postCollection == NestedPostCollection.Comments ? "id" : "user_id";
        var deletionScript = @"for(int i = ctx._source." + itemName + @".length - 1; i >= 0; i--) {
                    if(ctx._source." + itemName + @"[i]." + paramName + @" == params." + paramName + @") {
                        ctx._source." + itemName + @".remove(i);
                    }
                }";


        var updateResult = await Client.UpdateAsync<Post>(
            postId,
            u => u
                .Script(s => s
                    .Source(deletionScript)
                    .Params(parameters => parameters.Add(paramName, id))),
            cancellationToken);

        if (!updateResult.IsValid) throw new NotImplementedException();
    }
}