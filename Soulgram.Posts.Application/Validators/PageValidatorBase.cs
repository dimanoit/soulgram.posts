using FluentValidation;
using Soulgram.Posts.Application.Models.Common;

namespace Soulgram.Posts.Application.Validators
{
	internal abstract class PageValidatorBase<T> : AbstractValidator<T> where T : PageRequestBase
	{
		protected PageValidatorBase()
		{
			RuleFor(e => e.Take)
				.Must(arg => arg >= 0)
				.WithMessage(arg => $"{nameof(arg.Take)} must be greater than or equal to zero");

			RuleFor(e => e.Skip)
				.Must(arg => arg >= 0)
				.WithMessage(arg => $"{nameof(arg.Skip)} must be greater than or equal to zero");
		}
	}
}