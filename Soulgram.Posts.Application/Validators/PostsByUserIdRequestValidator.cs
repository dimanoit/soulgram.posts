using System.Linq;
using FluentValidation;
using Soulgram.Posts.Application.Models.Requests;

namespace Soulgram.Posts.Application.Validators;

internal class PostsByUserIdRequestValidator : PageValidatorBase<PostsByUserIdRequest>
{
    // TODO think about common UserId validation 
    public PostsByUserIdRequestValidator()
    {
        RuleFor(e => e.UsersIds)
            .Must(arg => arg.Any())
            .WithMessage(arg => $"{nameof(arg.UsersIds)} should have at least one non empty user id");
    }
}