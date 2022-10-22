using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nest;
using Soulgram.Posts.Application.Models.Requests;
using Soulgram.Posts.Application.Services;
using Soulgram.Posts.Domain;
using IRequest = MediatR.IRequest;

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
        private readonly IElasticClient _elasticClient;
        private readonly ICurrentDateProvider _dateProvider;

        public Handler(
            ICurrentDateProvider dateProvider,
            IElasticClient elasticClient)
        {
            _dateProvider = dateProvider;
            _elasticClient = elasticClient;
        }

        public async Task<Unit> Handle(AddCommentCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            
            // TODO move to convertor
            var comment = new Domain.Comment
            {
                UserId = request.UserId,
                Data = request.Text,

                State = PostState.Published,
                Type = DocumentType.Comment,
                CreationDate = _dateProvider.Now
            };
            
            // TODO create a wrapper for Elastic Client and refactor this duplication
            var updateResult = await _elasticClient.UpdateAsync<Domain.Post>(
                request.PostId,
                u => u.Script(s => s
                    .Source("ctx._source.comments.add(params.comment)")
                    .Params(parameters => parameters.Add("comment", comment))),
                cancellationToken);

            if (updateResult.IsValid)
            {
                return Unit.Value;
            }

            throw new NotImplementedException();
        }
    }
}