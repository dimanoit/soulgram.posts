using FluentValidation;
using Soulgram.Posts.Application.Models.Requests;
using System.Linq;

namespace Soulgram.Posts.Application.Validators
{
    internal class PostPublicationRequestValidator : AbstractValidator<PostPublicationRequest>
    {
        public PostPublicationRequestValidator()
        {
            RuleFor(e => e.UserInfo.Id)
                .Must(arg => !string.IsNullOrEmpty(arg))
                .WithMessage(arg => $"{nameof(arg.UserInfo.Id)} must be not empty");

            RuleFor(e => e)
                .Must(arg => !string.IsNullOrEmpty(arg.Text) || !arg.Medias.Any())
                .WithMessage(_ => "Post should has text or media");
        }
    }
}