using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Application.Commands.Like
{
	public class DeleteLikeCommand : IRequest
	{
		public DeleteLikeCommand(string userId)
		{
			UserId = userId;
		}
		public string UserId { get; }


		internal class Handler : IRequestHandler<DeleteLikeCommand>
		{
			public Task<Unit> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
			{
				return Unit.Task;
			}
		}
	}
}