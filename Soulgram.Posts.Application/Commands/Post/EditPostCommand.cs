using MediatR;
using Soulgram.Posts.Application.Models.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Application.Commands.Post
{
	public class EditPostCommand : IRequest
	{
		public EditPostCommand(PostPublicationRequest postPublicationRequest)
		{
			PostPublicationRequest = postPublicationRequest;
		}
		public PostPublicationRequest PostPublicationRequest { get; }


		internal class Handler : IRequestHandler<EditPostCommand>
		{
			public async Task<Unit> Handle(EditPostCommand request, CancellationToken cancellationToken)
			{
				return await Task.FromResult(Unit.Value);
			}
		}
	}
}