using Blog.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Settings
{
    public static class DbContextSettings
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
