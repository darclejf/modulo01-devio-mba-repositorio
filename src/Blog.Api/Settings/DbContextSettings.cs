using Blog.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Settings
{
    public static class DbContextSettings
    {
        public static WebApplicationBuilder AddDbContextSettings(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BlogDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            return builder;
        }
    }
}
