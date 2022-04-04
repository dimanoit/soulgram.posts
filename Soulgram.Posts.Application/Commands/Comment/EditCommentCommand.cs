using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Soulgram.Posts.Application.Models.Requests;

namespace Soulgram.Posts.Application.Commands.Comment;

public class EditCommentCommand : IRequest
{
    public EditCommentCommand(CommentPublicationRequest request)
    {
        Request = request;
    }

    public CommentPublicationRequest Request { get; }


    internal class Handler : IRequestHandler<EditCommentCommand>
    {
        public Task<Unit> Handle(EditCommentCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}