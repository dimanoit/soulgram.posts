using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Posts.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{postId}/count/{viewCount}")]
        public async Task AddViews(string postId, int viewCount, CancellationToken cancellationToken) =>
            await _mediator.Send(new AddViewCountCommand(postId: postId, viewCount: viewCount), cancellationToken);
    }
}
