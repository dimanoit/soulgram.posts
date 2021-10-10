using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Application.Commands
{
	public class AddViewCountCommand : IRequest
	{
		public AddViewCountCommand(string userId, string postId)
		{
			UserId = userId;
			PostId = postId;
		}
		public string UserId { get; }
		public string PostId { get; }


		internal class Handler : IRequestHandler<AddViewCountCommand>
		{
			public Task<Unit> Handle(AddViewCountCommand request, CancellationToken cancellationToken)
			{
				return Unit.Task;
			}
		}
	}
}