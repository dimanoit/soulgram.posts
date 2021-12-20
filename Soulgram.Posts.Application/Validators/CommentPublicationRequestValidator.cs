using System;
using FluentValidation;
using Soulgram.Posts.Application.Models.Requests;

namespace Soulgram.Posts.Application.Validators
{
	internal class CommentPublicationRequestValidator : AbstractValidator<CommentPublicationRequest>
	{
		public CommentPublicationRequestValidator()
		{
			RuleFor(e => e.UserId)
				.Must(arg => !string.IsNullOrEmpty(arg))
				.WithMessage(arg => $"{nameof(arg.UserId)} must be not empty");

			RuleFor(e => e.Text)
				.Must(arg => !string.IsNullOrEmpty(arg))
				.WithMessage(arg => $"{nameof(arg.Text)} should be empty");

			// TODO 200 in config
			RuleFor(e => e.Text)
				.Must(arg => arg.Length < 200)
				.WithMessage(arg => $"{nameof(arg.Text)} should be less that 200 symbols");

			RuleFor(e => e.Time)
				.Must(arg => arg.Equals(new DateTime()))
				.WithMessage(arg => $"{nameof(arg.Time)} should be not default");
		}
	}
}
