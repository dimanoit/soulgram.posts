using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Application.Commands.Post
{
	public class DeletePostCommand : IRequest
	{
		public DeletePostCommand(string postId)
		{
			PostId = postId;
		}
		public string PostId { get; }


		internal class Handler : IRequestHandler<DeletePostCommand>
		{
			public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
			{
				return await Unit.Task;
			}
		}
	}
}
