using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Posts.Application.Models;
using Soulgram.Posts.Application.Queries;
using System.Threading;
using System.Threading.Tasks;
using Soulgram.Posts.Application.Models.Requests;
using Soulgram.Posts.Application.Models.Responses;

namespace Soulgram.Posts.Api.Controllers
{
	[Route("api/[controller]")]
	public class SearchController : ControllerBase
	{
		private readonly IMediator _mediator;

		public SearchController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<PostsByIdResponse> GetAsync(PostsByIdRequest request, CancellationToken token)
		{
			return await _mediator.Send(
				new GetPostsByUserIdQuery(request),
				token);
		}
	}
}
