using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Soulgram.Posts.Application.Commands.Comment;

public class DeleteCommentCommand : IRequest
{
    public DeleteCommentCommand(string postId)
    {
        PostId = postId;
    }

    public string PostId { get; }


    internal class Handler : IRequestHandler<DeleteCommentCommand>
    {
        public Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}