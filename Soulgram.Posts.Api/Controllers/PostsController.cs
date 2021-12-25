using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Posts.Application.Commands.Post;
using Soulgram.Posts.Application.Models.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostsController : ControllerBase
	{

		private readonly IMediator _mediator;

		public PostsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("post")]
		public async Task AddPost(PostPublicationRequest request, CancellationToken cancellationToken) =>
			await _mediator.Send(new AddPostCommand(request), cancellationToken);

		[HttpPut("post")]
		public async Task EditPost(PostPublicationRequest request, CancellationToken cancellationToken) =>
			await _mediator.Send(new EditPostCommand(request), cancellationToken);

		[HttpDelete("post")]
		public async Task DeletePost(string postId, CancellationToken cancellationToken) =>
			await _mediator.Send(new DeletePostCommand(postId), cancellationToken);
	}
}
