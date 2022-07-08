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

public record GetPostsByUserIdQuery(PostsByUserIdRequest Request) : MediatR.IRequest<PostsByIdResponse>;

internal class GetPostsByUserIdQueryHandler : IRequestHandler<GetPostsByUserIdQuery, PostsByIdResponse>
{
    private readonly IElasticClient _client;

    public GetPostsByUserIdQueryHandler(IElasticClient client)
    {
        _client = client;
    }

    public async Task<PostsByIdResponse> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
    {
        // TODO make projection on server https://stackoverflow.com/questions/61320740/will-nest-project-in-elasticsearch-or-in-the-client
        Func<SearchDescriptor<Post>, ISearchRequest> query = searchDescriptor => searchDescriptor
            .Query(SearchQuerySelector(request))
            .Sort(sd => sd.Descending(p => p.CreationDate))
            .From(request.Request.Skip)
            .Size(request.Request.Take);

        var searchResult = await _client.SearchAsync(query, cancellationToken);
        var hits = searchResult.Hits;
        if (hits == null)
        {
            return null;
        }

        var response = new PostsByIdResponse
        {
            Data = hits.Select(h => h.ToEnrichedPost(request.Request.CurrentUserId)),
            TotalCount = (int) searchResult.Total
        };

        return response;
    }

    private static Func<QueryContainerDescriptor<Post>, QueryContainer> SearchQuerySelector(
        GetPostsByUserIdQuery request)
    {
        return q =>
            q.Term(p => p.UserId, request.Request.UserId)
            &&
            q.Term(p => p.Type, DocumentType.Post.ToString());
    }
}