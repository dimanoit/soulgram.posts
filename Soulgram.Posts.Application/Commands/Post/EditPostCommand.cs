using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nest;
using Soulgram.File.Manager.Interfaces;
using Soulgram.Posts.Application.Converters;
using Soulgram.Posts.Application.Models.Requests;
using Soulgram.Posts.Application.Services;
using IRequest = MediatR.IRequest;

namespace Soulgram.Posts.Application.Commands.Post;

public class EditPostCommand : IRequest
{
    private readonly PostUpdateRequest _postPublicationRequest;

    public EditPostCommand(PostUpdateRequest postPublicationRequest)
    {
        _postPublicationRequest = postPublicationRequest;
    }


    internal class Handler : IRequestHandler<EditPostCommand>
    {
        private readonly IElasticClient _client;
        private readonly ICurrentDateProvider _currentDateProvider;
        private readonly IFileManager _fileManager;

        public Handler(
            IElasticClient client,
            IFileManager fileManager,
            ICurrentDateProvider currentDateProvider)
        {
            _client = client;
            _fileManager = fileManager;
            _currentDateProvider = currentDateProvider;
        }

        public async Task<Unit> Handle(EditPostCommand request, CancellationToken cancellationToken)
        {
            //TODO mapping delete all properties
            var postToUpdate = await request._postPublicationRequest.ToPost(_fileManager, _currentDateProvider);

            var response = await _client.UpdateAsync<Domain.Post>(
                request._postPublicationRequest.PostId,
                _ => _.Doc(postToUpdate),
                cancellationToken);

            //TODO make common exception handling
            if (!response.IsValid) throw new Exception("Bla bla", response.OriginalException);

            return Unit.Value;
        }
    }
}