using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nest;
using Soulgram.File.Manager.Interfaces;
using Soulgram.Posts.Application.Mapper;
using Soulgram.Posts.Application.Models.Requests;

namespace Soulgram.Posts.Application.Commands.Post;

public class AddPostCommand : MediatR.IRequest<string>
{
    private readonly PostPublicationRequest _postPublicationRequest;

    public AddPostCommand(PostPublicationRequest postPublicationRequest)
    {
        _postPublicationRequest = postPublicationRequest;
    }

    internal class Handler : IRequestHandler<AddPostCommand, string>
    {
        private readonly IElasticClient _client;
        private readonly IFileManager _fileManager;

        public Handler(IElasticClient client, IFileManager fileManager)
        {
            _client = client;
            _fileManager = fileManager;
        }

        public async Task<string> Handle(AddPostCommand request, CancellationToken cancellationToken)
        {
            var post = await request._postPublicationRequest.ToPost(_fileManager);

            var response = await _client.IndexDocumentAsync(post, cancellationToken);

            if (!response.IsValid) throw new Exception("Can't add post", response.OriginalException);

            return response.Id;
        }
    }
}