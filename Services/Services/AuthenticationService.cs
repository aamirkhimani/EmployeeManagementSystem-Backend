using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.Models;
using Common.Models.Request;
using Common.Models.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Serilog;
using Services.Interface;

namespace Services.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly ILogger _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ApplicationSettings _applicationSettings;
        public readonly string source = nameof(AuthenticationService);

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationSettings applicationSettings, ILogger logger)
		{
			_applicationSettings = applicationSettings;
			_userManager = userManager;
			_roleManager = roleManager;
			_logger = logger;
		}

		public async Task<ResultDTO> Register(RegistrationRequest registerRequest)
		{
			var sourceMethod = $"{source}.{nameof(Register)}";

			_logger.Information($"{sourceMethod}:	started...");

			try
			{
				var user = await _userManager.FindByNameAsync(registerRequest.Username);

				if (user != null)
					return new ResultDTO
					{
						IsSuccessful = false,
						Message = "User with the provided username already exists.",
						StatusCode = System.Net.HttpStatusCode.BadRequest
					};

				ApplicationUser newUser = new()
				{
					UserName = registerRequest.Username,
					Email = registerRequest.Email,
					Name = $"{registerRequest.FirstName} {registerRequest.LastName}"
				};

				var createUserResult = await _userManager.CreateAsync(newUser, registerRequest.Password);

				if (!createUserResult.Succeeded)
					return new ResultDTO
					{
						IsSuccessful = false,
						Message = "User registration failed. Please check the details and try again later.",
						StatusCode = System.Net.HttpStatusCode.BadGateway
					};

				return new ResultDTO
				{
					IsSuccessful = true,
					Message = "User registered successfully",
					StatusCode = System.Net.HttpStatusCode.OK
				};
			}
			catch(Exception ex)
			{
				_logger.Error($"{sourceMethod}:	{ex.Message}");

                return new ResultDTO
                {
                    IsSuccessful = false,
                    Message = "An error occured",
                    StatusCode = System.Net.HttpStatusCode.BadGateway
                };
            }
        }

		public async Task<ResultDTO> Login(LoginRequest loginRequest)
		{
            var sourceMethod = $"{source}.{nameof(Login)}";

            _logger.Information($"{sourceMethod}:	started...");

			try
			{
				var user = await _userManager.FindByNameAsync(loginRequest.Username);

				if(user == null)
                    return new ResultDTO
                    {
                        IsSuccessful = false,
                        Message = "Incorrect username or password.",
                        StatusCode = System.Net.HttpStatusCode.BadRequest
                    };

				if(!await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                    return new ResultDTO
                    {
                        IsSuccessful = false,
                        Message = "Incorrect username or password.",
                        StatusCode = System.Net.HttpStatusCode.BadRequest
                    };

				string token = GenerateToken(loginRequest.Username);

                return new ResultDTO
                {
                    IsSuccessful = true,
                    Data = new LoginResponse() { Username = loginRequest.Username, Token = token, Expiry = DateTime.Now.AddMinutes(_applicationSettings.JwtSettings.ExpiryInMinutes)},
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
			catch(Exception ex)
			{
                _logger.Error($"{sourceMethod}:	{ex.Message}");

                return new ResultDTO
                {
                    IsSuccessful = false,
                    Message = "An error occured",
                    StatusCode = System.Net.HttpStatusCode.BadGateway
                };
            }
        }

		private string GenerateToken(string username)
		{
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettings.JwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, username),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                _applicationSettings.JwtSettings.Issuer,
                _applicationSettings.JwtSettings.Audience,
                authClaims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_applicationSettings.JwtSettings.ExpiryInMinutes)),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

