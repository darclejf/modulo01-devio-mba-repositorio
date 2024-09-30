using Blog.Data.Interfaces.Application.Models;

namespace Blog.Data.Interfaces.Application
{
    public interface IAuthenticationApplicationServices
    {
        Task<string> RegisterAsync(RegisterUserApplicationModel model); //TODO o parametro pode ser um model ou deveria ser um dto para utilizar em outros projetos?
    }
}
