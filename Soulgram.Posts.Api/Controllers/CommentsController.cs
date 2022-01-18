using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Posts.Application.Commands.Comment;
using Soulgram.Posts.Application.Models.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Api.Controllers
{
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
		public async Task AddComment(CommentPublicationRequest request, CancellationToken cancellationToken) =>
			await _mediator.Send(new AddCommentCommand(request), cancellationToken);

		[HttpPut]
		public async Task EditComment(CommentPublicationRequest request, CancellationToken cancellationToken) =>
			await _mediator.Send(new EditCommentCommand(request), cancellationToken);

		[HttpDelete]
		public async Task DeleteComment(string commentId, CancellationToken cancellationToken) =>
			await _mediator.Send(new DeleteCommentCommand(commentId), cancellationToken);
	}
}
