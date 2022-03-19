using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Nest;
using Soulgram.File.Manager;
using Soulgram.Posts.Application.Models.Requests;

namespace Soulgram.Posts.Application.Commands.Post;

public class AddPostCommand : MediatR.IRequest<string>
{
    public AddPostCommand(PostPublicationRequest postPublicationRequest)
    {
        PostPublicationRequest = postPublicationRequest;
    }

    private PostPublicationRequest PostPublicationRequest { get; }

    internal class Handler : IRequestHandler<AddPostCommand, string>
    {
        private readonly IElasticClient _client;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;

        public Handler(IElasticClient client, IMapper mapper, IFileManager fileManager)
        {
            _client = client;
            _mapper = mapper;
            _fileManager = fileManager;
        }

        public async Task<string> Handle(AddPostCommand request, CancellationToken cancellationToken)
        {
            var files = _mapper.Map<IEnumerable<FileInfo>>(request.PostPublicationRequest.Files);
            var linksOfUploadedFiles =
                await _fileManager.UploadFilesAndGetIds(files, request.PostPublicationRequest.UserId);

            var domainPost = _mapper.Map<Domain.Post>(request.PostPublicationRequest);
            domainPost.Medias = linksOfUploadedFiles;

            var response = await _client.IndexDocumentAsync(domainPost, cancellationToken);
            if (!response.IsValid) throw new Exception("Can't add post", response.OriginalException);

            return response.Id;
        }
    }
}