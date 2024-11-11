using Blog.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Blog.Application.Interfaces
{
	public interface IAuthenticationApplicationServices
    {
        Task<string> RegisterAsync(RegisterUserModel model); //TODO o parametro pode ser um model ou deveria ser um dto para utilizar em outros projetos?
        Task<IdentityResult> RegisterAuthorAsync(RegisterUserModel model);
        Task<string> LoginAsync(LoginUserModel model);
    }
}
