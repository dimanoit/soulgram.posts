using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Application.Commands
{
	public class AddViewCountCommand : IRequest
	{
		public AddViewCountCommand(string userId)
		{
			UserId = userId;
		}
		public string UserId { get; }


		internal class Handler : IRequestHandler<AddViewCountCommand>
		{
			public Task<Unit> Handle(AddViewCountCommand request, CancellationToken cancellationToken)
			{
				return Unit.Task;
			}
		}
	}
}