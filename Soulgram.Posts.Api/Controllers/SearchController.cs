using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Posts.Application.Models.Post;
using Soulgram.Posts.Application.Models.Requests;
using Soulgram.Posts.Application.Models.Responses;
using Soulgram.Posts.Application.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Soulgram.Posts.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("user")]
        public async Task<PostsByIdResponse> GetAsync(PostsByUserIdRequest request, CancellationToken token)
        {
            return await _mediator.Send(
                new GetPostsByUserIdQuery(request),
                token);
        }

        [HttpGet]
        public async Task<EnrichedPost> GetAsync(PostsByIdRequest request, CancellationToken token)
        {
            return await _mediator.Send(
                new GetPostsByIdQuery(request),
                token);
        }
    }
}
