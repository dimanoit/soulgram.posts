using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nest;
using Soulgram.Posts.Application.Mapper;
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
                .Query(query => query
                    .Term(term => term
                        .Field(field => field.UserId)
                        .Value(request.Request.UserId)
                    )
                ), cancellationToken);

            var hit = searchResult.Hits?.FirstOrDefault();
            if (hit == null) return null;

            var response = new PostsByIdResponse
            {
                Data = new[] {hit.ToEnrichedPost()},
                TotalCount = (int) searchResult.Total
            };

            return response;
        }
    }
}