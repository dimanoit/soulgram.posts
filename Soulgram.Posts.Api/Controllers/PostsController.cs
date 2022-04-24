using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Posts.Application.Commands.Post;
using Soulgram.Posts.Application.Models.Requests;

namespace Soulgram.Posts.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<string> AddPost([FromForm] PostPublicationRequest request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new AddPostCommand(request), cancellationToken);
    }

    [HttpPut]
    public async Task EditPost(PostUpdateRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new EditPostCommand(request), cancellationToken);
    }

    [HttpPatch]
    public async Task MarkAsDraft(PostUpdateRequest request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new EditPostCommand(request), cancellationToken);
    }
    
    [HttpDelete("{postId}")]
    public async Task DeletePost(string postId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeletePostCommand(postId), cancellationToken);
    }
}