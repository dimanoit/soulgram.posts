using MediatR;
using Soulgram.Posts.Application.Models.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Application.Commands.Post
{
	public class AddPostCommand : IRequest
	{
		public AddPostCommand(PostPublicationRequest postPublicationRequest)
		{
			PostPublicationRequest = postPublicationRequest;
		}

		public PostPublicationRequest PostPublicationRequest { get; }


		internal class Handler : IRequestHandler<AddPostCommand>
		{
			public async Task<Unit> Handle(AddPostCommand request, CancellationToken cancellationToken)
			{
				return await Task.FromResult(Unit.Value);
			}
		}
	}
}