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

		[HttpPatch("{postId}")]
		public async Task AddViews(string postId, string userId, CancellationToken cancellationToken) =>
			await _mediator.Send(new AddViewCountCommand(userId: userId, postId: postId), cancellationToken);
	}
}
