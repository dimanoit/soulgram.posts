using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Application.Commands.Like
{
	public class AddLikeCommand : IRequest
	{
		public AddLikeCommand(string userId)
		{
			UserId = userId;
		}
		public string UserId { get; }


		internal class Handler : IRequestHandler<AddLikeCommand>
		{
			public Task<Unit> Handle(AddLikeCommand request, CancellationToken cancellationToken)
			{
				return Unit.Task;
			}
		}
	}
}