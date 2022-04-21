using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nest;
using IRequest = MediatR.IRequest;

namespace Soulgram.Posts.Application.Commands;

public class AddViewCountCommand : IRequest
{
    public AddViewCountCommand(string postId, int viewCount)
    {
        PostId = postId;
        ViewCount = viewCount;
    }

    public string PostId { get; }
    public int ViewCount { get; }

    internal class Handler : IRequestHandler<AddViewCountCommand>
    {
        private readonly IElasticClient _client;

        public Handler(IElasticClient client)
        {
            _client = client;
        }

        public async Task<Unit> Handle(AddViewCountCommand request, CancellationToken cancellationToken)
        {
            var partPostToUpdate = new Domain.Post();

            var response = await _client.UpdateAsync<Domain.Post>(
                request.PostId,
                _ => _.Doc(partPostToUpdate),
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