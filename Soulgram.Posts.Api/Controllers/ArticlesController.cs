using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Soulgram.Posts.Application.Commands.Post;
using Soulgram.Posts.Application.Models.Requests;

namespace Soulgram.Posts.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ArticlesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<string> AddArticle(ArticlePublicationRequest request, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new AddArticleCommand(request), cancellationToken);
    }
}