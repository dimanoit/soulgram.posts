using System;
using MediatR;
using Soulgram.Posts.Application.Models.Requests;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Nest;
using IRequest = MediatR.IRequest;

namespace Soulgram.Posts.Application.Commands.Post
{
    public class EditPostCommand : IRequest
    {
        public EditPostCommand(PostUpdateRequest postPublicationRequest)
        {
            PostPublicationRequest = postPublicationRequest;
        }

        public PostUpdateRequest PostPublicationRequest { get; }


        internal class Handler : IRequestHandler<EditPostCommand>
        {
            private readonly IMapper _mapper;
            private readonly IElasticClient _client;

            public Handler(IElasticClient client, IMapper mapper)
            {
                _client = client;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(EditPostCommand request, CancellationToken cancellationToken)
            {
                var postToUpdate = _mapper.Map<Domain.Post>(request.PostPublicationRequest);

                var response = await _client.UpdateAsync<Domain.Post>(
                    request.PostPublicationRequest.PostId,
                    _ => _.Doc(postToUpdate),
                    cancellationToken);

                //TODO make common exception handling
                if (!response.IsValid)
                {
                    throw new Exception("Bla bla", response.OriginalException);
                }

                return Unit.Value;
            }
        }
    }
}