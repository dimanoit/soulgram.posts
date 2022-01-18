using FluentValidation;
using Soulgram.Posts.Application.Models.Requests;

namespace Soulgram.Posts.Application.Validators
{
	internal class PostsByUserIdRequestValidator : PageValidatorBase<PostsByUserIdRequest>
	{
		// TODO think about common UserId validation 
		public PostsByUserIdRequestValidator() =>
			RuleFor(e => e.UserId)
				.Must(arg => !string.IsNullOrEmpty(arg))
				.WithMessage(arg => $"{nameof(arg.UserId)} must be not empty");
	}
}
