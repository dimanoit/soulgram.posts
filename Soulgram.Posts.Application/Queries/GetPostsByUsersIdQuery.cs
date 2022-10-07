using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nest;
using Soulgram.Posts.Application.Converters;
using Soulgram.Posts.Application.Models.Requests;
using Soulgram.Posts.Application.Models.Responses;
using Soulgram.Posts.Domain;

namespace Soulgram.Posts.Application.Queries;

public record GetPostsByUsersIdQuery(PostsByUserIdRequest Request) : MediatR.IRequest<PostsByIdResponse>;

internal class GetPostsByUsersIdQueryHandler : IRequestHandler<GetPostsByUsersIdQuery, PostsByIdResponse>
{
    private readonly IElasticClient _client;

    public GetPostsByUsersIdQueryHandler(IElasticClient client)
    {
        _client = client;
    }

    public async Task<PostsByIdResponse> Handle(GetPostsByUsersIdQuery request, CancellationToken cancellationToken)
    {
        // TODO make projection on server https://stackoverflow.com/questions/61320740/will-nest-project-in-elasticsearch-or-in-the-client
        Func<SearchDescriptor<Post>, ISearchRequest> query = searchDescriptor => searchDescriptor
            .Query(SearchQuerySelector(request))
            .Sort(sd => sd.Descending(p => p.CreationDate))
            .From(request.Request.Skip)
            .Size(request.Request.Take);

        var searchResult = await _client.SearchAsync(query, cancellationToken);
        var hits = searchResult.Hits;
        if (hits == null) return null;

        var response = new PostsByIdResponse
        {
            Data = hits.Select(h => h.ToEnrichedPost(request.Request.CurrentUserId)),
            TotalCount = (int)searchResult.Total
        };

        return response;
    }

    private static Func<QueryContainerDescriptor<Post>, QueryContainer> SearchQuerySelector(
        GetPostsByUsersIdQuery request)
    {
        return q =>
            q.Term(p => p.Type, DocumentType.Post.ToString())
            &&
            // Get posts with specific users ids
            q.Terms(c =>
                c.Name("users_posts")
                    .Boost(1.0)
                    .Field(p => p.UserId)
                    .Terms(request.Request.UsersIds));
    }
}