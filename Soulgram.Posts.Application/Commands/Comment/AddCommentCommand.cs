using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Soulgram.Posts.Application.Models.Requests;

namespace Soulgram.Posts.Application.Commands.Comment;

public class AddCommentCommand : IRequest
{
    public AddCommentCommand(CommentPublicationRequest request)
    {
        Request = request;
    }

    public CommentPublicationRequest Request { get; }


    internal class Handler : IRequestHandler<AddCommentCommand>
    {
        public Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            return Unit.Task;
        }
    }
}