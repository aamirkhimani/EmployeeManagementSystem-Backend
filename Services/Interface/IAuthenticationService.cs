using System;
using Common.Models;
using Common.Models.Request;

namespace Services.Interface
{
	public interface IAuthenticationService
	{
		Task<ResultDTO> Register(RegistrationRequest registerRequest);

		Task<ResultDTO> Login(LoginRequest loginRequest);

    }
}

