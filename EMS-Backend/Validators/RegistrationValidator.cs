using System;
using Common;
using Common.Models.Request;
using FluentValidation;

namespace EMS_Backend.Validators
{
	public class RegistrationValidator: AbstractValidator<RegistrationRequest>
	{
		public RegistrationValidator()
		{
			RuleFor(registrationRequest => registrationRequest.FirstName).NotNull()
				.NotEmpty()
				.Matches(Constants.NameRegex);

			RuleFor(registrationRequest => registrationRequest.LastName).NotNull()
				.NotEmpty()
				.Matches(Constants.NameRegex);

			RuleFor(registrationRequest => registrationRequest.Email).NotNull()
				.NotEmpty()
				.Matches(Constants.EmailRegex);

			RuleFor(registrationRequest => registrationRequest.Username).NotNull()
				.NotEmpty()
				.Matches(Constants.UsernameRegex);

			RuleFor(registrationRequest => registrationRequest.Password).NotNull()
				.NotEmpty()
				.Matches(Constants.PasswordRegex);
		}

		
	}
}

