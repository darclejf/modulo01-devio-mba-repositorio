using Blog.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Settings
{
    public static class DbContextSettings //TODO é correto repetir o settings para cada projeto,
                                          //pois o appsetings pode estar com estrutura e chaves de propriedades diferentes para cada um?
    {
        public static WebApplicationBuilder AddDbContextSettings(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDbContext<BlogDbContext>(options =>
                    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
            }
            else
            {
                builder.Services.AddDbContext<BlogDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                                .LogTo(Console.WriteLine, LogLevel.Information);
                });
            }
            return builder;
        }
    }
}
