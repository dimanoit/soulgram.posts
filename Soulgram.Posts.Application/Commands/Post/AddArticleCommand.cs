using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nest;
using Soulgram.Posts.Application.Converters;
using Soulgram.Posts.Application.Models.Requests;

namespace Soulgram.Posts.Application.Commands.Post;

public class AddArticleCommand : MediatR.IRequest<string>
{
    private readonly ArticlePublicationRequest _articlePublicationRequest;

    public AddArticleCommand(ArticlePublicationRequest articlePublicationRequest)
    {
        _articlePublicationRequest = articlePublicationRequest;
    }

    internal class Handler : IRequestHandler<AddArticleCommand, string>
    {
        private readonly IElasticClient _client;

        public Handler(IElasticClient client)
        {
            _client = client;
        }

        public async Task<string> Handle(AddArticleCommand request, CancellationToken cancellationToken)
        {
            var article = request._articlePublicationRequest.ToArticle();

            var response = await _client.IndexDocumentAsync(article, cancellationToken);

            if (!response.IsValid) throw new Exception("Can't add post", response.OriginalException);

            return response.Id;
        }
    }
}