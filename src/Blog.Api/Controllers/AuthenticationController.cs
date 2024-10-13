using Blog.Data.Interfaces.Application;
using Blog.Data.Models;
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
    }
}