using System;
using Common;
using Common.Models.Request;
using FluentValidation;

namespace EMS_Backend.Validators
{
	public class LoginValidator : AbstractValidator<LoginRequest>
	{
		public LoginValidator()
		{
			RuleFor(loginRequest => loginRequest.Username).Matches(Constants.UsernameRegex);
			RuleFor(loginRequest => loginRequest.Password).Matches(Constants.PasswordRegex);
		}
	}
}

