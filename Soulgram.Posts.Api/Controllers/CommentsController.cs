using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Posts.Application.Commands.Comment;
using Soulgram.Posts.Application.Models.Requests;

namespace Soulgram.Posts.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task AddComment(CommentPublicationRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new AddCommentCommand(request), cancellationToken);
    }

    [HttpDelete("{commentId}/posts/{postId}")]
    public async Task DeleteComment(string commentId, string postId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteCommentCommand(postId, commentId), cancellationToken);
    }
}