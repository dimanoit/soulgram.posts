using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Soulgram.Posts.Application.Commands.Like;

public class DeleteLikeCommand : IRequest
{
    public DeleteLikeCommand(string userId, string postId)
    {
        UserId = userId;
        PostId = postId;
    }

    public string UserId { get; }
    public string PostId { get; }


    internal class Handler : IRequestHandler<DeleteLikeCommand>
    {
        public Task<Unit> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}