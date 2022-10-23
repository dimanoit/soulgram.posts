using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Soulgram.Posts.Application.Models.Requests;
using Soulgram.Posts.Application.Services;
using Soulgram.Posts.Domain;
using Soulgram.Posts.Persistence;

namespace Soulgram.Posts.Application.Commands.Comment;

public class AddCommentCommand : IRequest
{
    public AddCommentCommand(CommentPublicationRequest request)
    {
        Request = request;
    }

    public CommentPublicationRequest Request { get; }


    internal class Handler : IRequestHandler<AddCommentCommand>
    {
        private readonly ICurrentDateProvider _dateProvider;
        private readonly IElasticClientDecorator _elasticClientDecorator;

        public Handler(
            ICurrentDateProvider dateProvider,
            IElasticClientDecorator elasticClientDecorator)
        {
            _dateProvider = dateProvider;
            _elasticClientDecorator = elasticClientDecorator;
        }

        public async Task<Unit> Handle(AddCommentCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            // TODO move to convertor
            var comment = new Domain.Comment
            {
                UserId = request.UserId,
                Data = request.Text,

                Id = Guid.NewGuid().ToString(),
                State = PostState.Published,
                Type = DocumentType.Comment,
                CreationDate = _dateProvider.Now
            };

            await _elasticClientDecorator
                .AddNestedCollectionItem(
                    request.PostId,
                    comment,
                    NestedPostCollection.Comments,
                    cancellationToken);

            return Unit.Value;
        }
    }
}