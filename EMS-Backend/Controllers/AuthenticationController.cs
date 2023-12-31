using Common.Models;
using Common.Models.Request;
using Common.Models.Response;
using Elfie.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using ILogger = Serilog.ILogger;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        public readonly string source = nameof(AuthenticationController);

        private readonly ILogger _logger;
        private readonly IAuthenticationService _authenticationService;
        private readonly IValidator<LoginRequest> _loginValidator;
        private readonly IValidator<RegistrationRequest> _registrationValidator;

        public AuthenticationController(IAuthenticationService authenticationService, ILogger logger, IValidator<LoginRequest> loginValidator, IValidator<RegistrationRequest> registrationValidator)
        {
            _logger = logger;
            _authenticationService = authenticationService;
            _loginValidator = loginValidator;
            _registrationValidator = registrationValidator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationRequest)
        {
            string methodContext = $"{source}.{nameof(Register)}";

            var validationResult = _registrationValidator.Validate(registrationRequest);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(errors => errors.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var result = await _authenticationService.Register(registrationRequest);

            return new ObjectResult(new { Message = result.Message })
            {
                StatusCode = Convert.ToInt32(result.StatusCode)
            };
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            string methodContext = $"{source}.{nameof(Login)}";

            var validationResult = _loginValidator.Validate(loginRequest);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var result = await _authenticationService.Login(loginRequest);

            return new ObjectResult(new { Message = result.Message, data = result.Data })
            {
                StatusCode = Convert.ToInt32(result.StatusCode)
            };
        }
    }
}

