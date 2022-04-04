using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Soulgram.Posts.Application.Commands.Like;

public class AddLikeCommand : IRequest
{
    public AddLikeCommand(string userId, string postId)
    {
        UserId = userId;
        PostId = postId;
    }

    public string UserId { get; }
    public string PostId { get; }


    internal class Handler : IRequestHandler<AddLikeCommand>
    {
        public Task<Unit> Handle(AddLikeCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}