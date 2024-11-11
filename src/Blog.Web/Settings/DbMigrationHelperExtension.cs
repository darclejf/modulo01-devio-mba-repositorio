using Blog.Data.Contexts;
using Blog.Data.Entities;
using Microsoft.AspNetCore.Identity;
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
            if (context.Roles.Any())
				return;

            var adminRoleId = Guid.NewGuid().ToString();
			await context.Roles.AddAsync(new IdentityRole
            {
                Id = adminRoleId,
                Name = BlogConstants.ADMINROLE,
                NormalizedName = BlogConstants.ADMINROLE,
            });

			await context.Roles.AddAsync(new IdentityRole
			{
				Id = Guid.NewGuid().ToString(),
				Name = BlogConstants.AUTHORROLE,
				NormalizedName = BlogConstants.AUTHORROLE,   
			});

			await context.SaveChangesAsync();

			if (context.Users.Any())
				return;

            var adminId = Guid.NewGuid().ToString();
            var userAdmin = 
			await context.Users.AddAsync(new IdentityUser
			{
				Id = adminId,
				UserName = "admin@admin.com",
				NormalizedUserName = "ADMIN@ADMIN.COM",
				Email = "admin@admin.com",
				NormalizedEmail = "ADMIN@ADMIN.COM",
				AccessFailedCount = 0,
				LockoutEnabled = false,
				PasswordHash = "AQAAAAIAAYagAAAAEEdWhqiCwW/jZz0hEM7aNjok7IxniahnxKxxO5zsx2TvWs4ht1FUDnYofR8JKsA5UA==",
				TwoFactorEnabled = false,
				ConcurrencyStamp = Guid.NewGuid().ToString(),
				EmailConfirmed = true,
				SecurityStamp = Guid.NewGuid().ToString(),
			});

			await context.SaveChangesAsync();

			await context.UserRoles.AddAsync(new IdentityUserRole<string>
			{
				RoleId = adminRoleId,
				UserId = adminId,
			});

			var user = context.Users.FirstOrDefault(x => x.Id == adminId);
            await context.Authors.AddAsync(Author.Create("Adminitstrador", user));

			await context.SaveChangesAsync();
		}
    }
}
