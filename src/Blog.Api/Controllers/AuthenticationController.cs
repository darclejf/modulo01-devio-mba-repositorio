using Blog.Application.Interfaces;
using Blog.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationApplicationServices _authenticationApplication;

        public AuthenticationController(IAuthenticationApplicationServices authenticationApplication)
        {
            _authenticationApplication = authenticationApplication;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel request)
        {
            if (!ModelState.IsValid) 
                return ValidationProblem(ModelState);

            var result = await _authenticationApplication.RegisterAsync(request);
            if (string.IsNullOrEmpty(result))
                return Problem("Falha ao registrar o usuário");

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserModel request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _authenticationApplication.LoginAsync(request);
            if (string.IsNullOrEmpty(result))
                return Problem("Usuário ou senha incorretos");
            return Ok(result);            
        }
    }
}