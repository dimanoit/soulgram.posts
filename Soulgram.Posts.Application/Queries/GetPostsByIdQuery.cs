using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nest;
using Soulgram.Posts.Application.Mapper;
using Soulgram.Posts.Application.Models.Post;
using Soulgram.Posts.Application.Models.Requests;
using Soulgram.Posts.Domain;

namespace Soulgram.Posts.Application.Queries;

public class GetPostsByIdQuery : MediatR.IRequest<EnrichedPost>
{
    public GetPostsByIdQuery(PostsByIdRequest request)
    {
        Request = request;
    }

    public PostsByIdRequest Request { get; }


    internal class Handler : IRequestHandler<GetPostsByIdQuery, EnrichedPost>
    {
        private readonly IElasticClient _client;

        public Handler(IElasticClient client)
        {
            _client = client;
        }

        public async Task<EnrichedPost> Handle(GetPostsByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync<Post>(request.Request.Id, ct: cancellationToken);
            if (!response.IsValid) throw new Exception("Bla bla bla", response.OriginalException);

            return !response.Found ? null : response.ToEnrichedPost();
        }
    }
}