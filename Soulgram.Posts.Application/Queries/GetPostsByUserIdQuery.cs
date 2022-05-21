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

public class GetPostsByUserIdQuery : MediatR.IRequest<PostsByIdResponse>
{
    public GetPostsByUserIdQuery(PostsByUserIdRequest request)
    {
        Request = request;
    }

    public PostsByUserIdRequest Request { get; }


    internal class Handler : IRequestHandler<GetPostsByUserIdQuery, PostsByIdResponse>
    {
        private readonly IElasticClient _client;

        public Handler(IElasticClient client)
        {
            _client = client;
        }

        public async Task<PostsByIdResponse> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await _client.SearchAsync<Post>(x => x
                .Query(SearchQuerySelector(request))
                .Sort(sd => sd.Descending(p => p.CreationDate)), cancellationToken);
            var hits = searchResult.Hits;
            if (hits == null) return null;

            var response = new PostsByIdResponse
            {
                Data = hits.Select(h => h.ToEnrichedPost()),
                TotalCount = (int)searchResult.Total
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
}