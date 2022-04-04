using System.Linq;
using FluentValidation;
using Soulgram.Posts.Application.Models.Requests;

namespace Soulgram.Posts.Application.Validators;

internal class PostPublicationRequestValidator : AbstractValidator<PostPublicationRequest>
{
    public PostPublicationRequestValidator()
    {
        RuleFor(e => e)
            .Must(arg => !string.IsNullOrEmpty(arg.Text) || !arg.Medias.Any())
            .WithMessage(_ => "Post should has text or media");
    }
}