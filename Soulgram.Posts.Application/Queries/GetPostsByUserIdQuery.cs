using MediatR;
using Soulgram.Posts.Application.Models.Requests;
using Soulgram.Posts.Application.Models.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Application.Queries
{
	public class GetPostsByUserIdQuery : IRequest<PostsByIdResponse>
	{
		public GetPostsByUserIdQuery(PostsByUserIdRequest request)
		{
			Request = request;
		}
		public PostsByUserIdRequest Request { get; }


		internal class Handler : IRequestHandler<GetPostsByUserIdQuery, PostsByIdResponse>
		{
			public async Task<PostsByIdResponse> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
			{
				return await Task.FromResult(new PostsByIdResponse());
			}
		}
	}
}