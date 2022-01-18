using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Nest;
using IRequest = MediatR.IRequest;

namespace Soulgram.Posts.Application.Commands.Post
{
    public class DeletePostCommand : IRequest
    {
        public DeletePostCommand(string postId)
        {
            PostId = postId;
        }
        public string PostId { get; }


        internal class Handler : IRequestHandler<DeletePostCommand>
        {
            private readonly IElasticClient _client;

            public Handler(IElasticClient client)
            {
                _client = client;
            }

            public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
            {
                var response = await _client.DeleteAsync<Domain.Post>(request.PostId, ct: cancellationToken);
                if (!response.IsValid)
                {
                    throw new Exception("Can't delete post", response.OriginalException);
                }

                return await Unit.Task;
            }
        }
    }
}
