using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Posts.Application.Commands;
using Soulgram.Posts.Application.Commands.Comment;
using Soulgram.Posts.Application.Commands.Like;
using Soulgram.Posts.Application.Commands.Post;
using Soulgram.Posts.Application.Models.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class ModificationController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ModificationController(IMediator mediator)
		{
			_mediator = mediator;
		}

		#region Posts
		[HttpPost("post")]
		public async Task AddPost(PostPublicationRequest request, CancellationToken cancellationToken) =>
			await _mediator.Send(new AddPostCommand(request), cancellationToken);

		[HttpPut("post")]
		public async Task EditPost(PostPublicationRequest request, CancellationToken cancellationToken) =>
			await _mediator.Send(new EditPostCommand(request), cancellationToken);

		[HttpDelete("post")]
		public async Task DeletePost(string postId, CancellationToken cancellationToken) =>
			await _mediator.Send(new DeletePostCommand(postId), cancellationToken);
		#endregion

		#region Comments
		[HttpPost("comment")]
		public async Task AddComment(CommentPublicationRequest request, CancellationToken cancellationToken) =>
			await _mediator.Send(new AddCommentCommand(request), cancellationToken);

		[HttpPut("comment")]
		public async Task EditComment(CommentPublicationRequest request, CancellationToken cancellationToken) =>
			await _mediator.Send(new EditCommentCommand(request), cancellationToken);

		[HttpDelete("comment")]
		public async Task DeleteComment(string commentId, CancellationToken cancellationToken) =>
			await _mediator.Send(new DeleteCommentCommand(commentId), cancellationToken);
		#endregion

		#region Like
		[HttpPut("post/{postId}/like")]
		public async Task AddLike(string postId, string userId, CancellationToken cancellationToken) =>
			await _mediator.Send(new AddLikeCommand(userId: userId, postId: postId), cancellationToken);

		[HttpDelete("post/{postId}/like")]
		public async Task DeleteLike(string postId, string userId, CancellationToken cancellationToken) =>
			await _mediator.Send(new DeleteLikeCommand(postId, userId), cancellationToken);

		#endregion

		#region Views
		[HttpPatch("post/{postId}/views")]
		public async Task AddViews(string postId, string userId, CancellationToken cancellationToken) =>
			await _mediator.Send(new AddViewCountCommand(userId: userId, postId: postId), cancellationToken);
		#endregion
	}
}
