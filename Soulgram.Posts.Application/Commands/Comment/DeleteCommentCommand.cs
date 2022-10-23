using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Soulgram.Posts.Persistence;

namespace Soulgram.Posts.Application.Commands.Comment;

public class DeleteCommentCommand : IRequest
{
    public DeleteCommentCommand(string postId, string commentId)
    {
        PostId = postId;
        CommentId = commentId;
    }

    public string PostId { get; }
    public string CommentId { get; }


    internal class Handler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly IElasticClientDecorator _elasticClientDecorator;

        public Handler(IElasticClientDecorator elasticClientDecorator)
        {
            _elasticClientDecorator = elasticClientDecorator;
        }

        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            await _elasticClientDecorator.DeleteNestedCollectionItem(request.PostId, request.CommentId,
                NestedPostCollection.Comments, cancellationToken);

            return Unit.Value;
        }
    }
}