using FluentValidation;
using Soulgram.Posts.Application.Models.Requests;

namespace Soulgram.Posts.Application.Validators;

internal class PostsByIdRequestValidator : AbstractValidator<PostsByIdRequest>
{
    // TODO think about common UserId validation 
    public PostsByIdRequestValidator()
    {
        RuleFor(e => e.Id)
            .Must(arg => !string.IsNullOrEmpty(arg))
            .WithMessage(arg => $"{nameof(arg.Id)} must be not empty");
    }
}