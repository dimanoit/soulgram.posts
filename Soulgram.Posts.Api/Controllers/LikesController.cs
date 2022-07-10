using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Posts.Application.Commands.Like;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LikesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LikesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{postId}/users/{userId}")]
    public async Task AddLike(
        [FromRoute] string postId,
        [FromRoute] string userId,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new AddLikeCommand(userId, postId), cancellationToken);
    }

    [HttpDelete("{postId}/users/{userId}")]
    public async Task DeleteLike(
        [FromRoute] string postId,
        [FromRoute] string userId,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteLikeCommand(userId, postId), cancellationToken);
    }
}