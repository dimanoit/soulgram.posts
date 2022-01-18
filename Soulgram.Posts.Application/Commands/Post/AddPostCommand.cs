using AutoMapper;
using MediatR;
using Nest;
using Soulgram.Posts.Application.Models.Requests;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Application.Commands.Post
{
    public class AddPostCommand : MediatR.IRequest<string>
    {
        public AddPostCommand(PostPublicationRequest postPublicationRequest)
        {
            PostPublicationRequest = postPublicationRequest;
        }

        public PostPublicationRequest PostPublicationRequest { get; }


        internal class Handler : IRequestHandler<AddPostCommand, string>
        {
            private readonly IElasticClient _client;
            private readonly IMapper _mapper;

            public Handler(IElasticClient client, IMapper mapper)
            {
                _client = client;
                _mapper = mapper;
            }

            public async Task<string> Handle(AddPostCommand request, CancellationToken cancellationToken)
            {
                var domainPost = _mapper.Map<Domain.Post>(request.PostPublicationRequest);
                var response = await _client.IndexDocumentAsync(domainPost, cancellationToken);
                if (!response.IsValid)
                {
                    throw new Exception("Can't add post", response.OriginalException);
                }

                return response.Id;
            }
        }
    }
}