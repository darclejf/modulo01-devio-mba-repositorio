using Blog.Data.Contexts;
using Microsoft.AspNetCore.Identity;

namespace Blog.Web.Settings
{
    public static class IdentitySettings
    {
        public static WebApplicationBuilder AddIdentitySettings(this WebApplicationBuilder builder)
        {
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BlogDbContext>();

            return builder;
        }
    }
}
