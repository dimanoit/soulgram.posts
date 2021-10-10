using MediatR;
using Soulgram.Posts.Application.Models.Post;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Application.Queries
{
	public class GetCommentsByPostIdQuery : IRequest<Comment>
	{
		public GetCommentsByPostIdQuery(string postId)
		{
			PostId = postId;
		}
		public string PostId { get; }


		internal class Handler : IRequestHandler<GetCommentsByPostIdQuery, Comment>
		{
			public Task<Comment> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
			{
				return Task.FromResult(new Comment());
			}
		}
	}
}