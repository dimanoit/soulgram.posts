using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Posts.Application.Commands.Like;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LikesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public LikesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPut("post/{postId}/like")]
		public async Task AddLike(string postId, string userId, CancellationToken cancellationToken) =>
			await _mediator.Send(new AddLikeCommand(userId: userId, postId: postId), cancellationToken);

		[HttpDelete("post/{postId}/like")]
		public async Task DeleteLike(string postId, string userId, CancellationToken cancellationToken) =>
			await _mediator.Send(new DeleteLikeCommand(postId, userId), cancellationToken);
	}
}
