using Blog.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Settings
{
    public static class DbMigrationHelperExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationHelpers.EnsureSeedData(app).Wait();
        }
    }

    public static class DbMigrationHelpers
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<BlogDbContext>();

            if (env.IsDevelopment())
            {
                await context.Database.MigrateAsync();
                await EnsureSeedProducts(context);
            }
        }

        private static async Task EnsureSeedProducts(BlogDbContext context)
        {
            //TODO administrador
        }
    }
}
