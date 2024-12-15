using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlayReviewApp.PlayReviewDb.DataAccess.Concrete.EntityFramework;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
        public static void ConfigureMigration(this IApplicationBuilder app)
        {
            PlayReviewDbContext context = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<PlayReviewDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        public static void ConfigureErrorCodes(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error");  // Üretim ortamında hata sayfasına yönlendir
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("/Error/Error/StatusCode?code={0}");
        }

        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization(options =>
            {
                options.AddSupportedCultures("tr-TR").AddSupportedUICultures("tr-TR").SetDefaultCulture("tr-TR");
            });
        }

        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
        {
            const string adminUser = "Admin";
            const string adminPassword = "Admin3541";

            UserManager<IdentityUser> userManager = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();

            RoleManager<IdentityRole> roleManager = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if (user is null)
            {
                user = new IdentityUser()
                {
                    Email = "ta.aktas@hotmail.com",
                    PhoneNumber = "55534567",
                    UserName = adminUser
                };

                var result = await userManager.CreateAsync(user, adminPassword);

                if (!result.Succeeded) throw new Exception("Admin user could not created.");

                var roleResult = await userManager.AddToRolesAsync(user,
                      roleManager.Roles.Select(r => r.Name).ToList()
                );

                if (!roleResult.Succeeded) throw new Exception("System have problems with role defination for admin");
            }
        }
    }
}
