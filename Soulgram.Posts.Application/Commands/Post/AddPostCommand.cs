using MediatR;
using Nest;
using Soulgram.Posts.Application.Models.Requests;
using System.Threading;
using System.Threading.Tasks;
using IRequest = MediatR.IRequest;

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

            public Handler(IElasticClient client)
            {
                _client = client;
            }

            public async Task<string> Handle(AddPostCommand request, CancellationToken cancellationToken)
            {
                return await Task.FromResult("kek");
            }
        }
    }
}