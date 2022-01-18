using System;
using MediatR;
using Soulgram.Posts.Application.Models.Post;
using Soulgram.Posts.Application.Models.Requests;
using Soulgram.Posts.Application.Models.Responses;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Nest;

namespace Soulgram.Posts.Application.Queries
{
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
            private readonly IMapper _mapper;

            public Handler(IElasticClient client, IMapper mapper)
            {
                _client = client;
                _mapper = mapper;
            }

            public async Task<EnrichedPost> Handle(GetPostsByIdQuery request, CancellationToken cancellationToken)
            {
                var response = await _client.GetAsync<Domain.Post>(request.Request.Id, ct: cancellationToken);
                if (!response.IsValid)
                {
                    throw new Exception("Bla bla bla", response.OriginalException);
                }

                if (!response.Found)
                {
                    return null;
                }

                var enrichedPost = _mapper.Map<EnrichedPost>(response.Source);
                return enrichedPost;
            }
        }
    }
}
