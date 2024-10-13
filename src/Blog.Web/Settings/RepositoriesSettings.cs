using Blog.Data.Application;
using Blog.Data.Interfaces.Application;
using Blog.Data.Interfaces.Repositories;
using Blog.Data.Repositories;

namespace Blog.Web.Settings
{
    public static class RepositoriesSettings
    {
        public static WebApplicationBuilder AddRepositoresSettings(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

            builder.Services.AddScoped<IAuthenticationApplicationServices, AuthenticationApplicationServices>();
            builder.Services.AddScoped<IPostApplicationServices, PostApplicationServices>();

            return builder;
        }
    }
}
