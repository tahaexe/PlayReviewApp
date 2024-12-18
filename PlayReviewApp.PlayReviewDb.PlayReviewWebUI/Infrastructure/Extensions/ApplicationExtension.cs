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

            string[] roles = { "ADMIN", "EDITOR", "USER" };

            // Her rolü kontrol et ve yoksa oluştur
            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!roleResult.Succeeded)
                        throw new Exception($"{role} role could not be created.");
                }
            }

            // Admin kullanıcısını kontrol et
            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if (user is null)
            {
                user = new IdentityUser()
                {
                    Email = "ta.aktas@hotmail.com",
                    PhoneNumber = "000000000",
                    UserName = adminUser
                };

                // Kullanıcıyı oluştur
                var result = await userManager.CreateAsync(user, adminPassword);

                if (!result.Succeeded)
                    throw new Exception("Admin user could not be created.");

                // Kullanıcıyı admin rolüne ekle
                var roleResult = await userManager.AddToRolesAsync(user, roles);
                if (!roleResult.Succeeded)
                    throw new Exception("System have problems with role definition for admin");
            }
        }


        public static void ConfigureRouting(this WebApplication applicationBuilder)
        {
            //Areas
            applicationBuilder.MapAreaControllerRoute(name: "Admin", areaName: "Admin", pattern: "Admin/{controller=Dasboard}/{action=index}/{id?}");

            //Routes
            applicationBuilder.MapControllerRoute(name: "News", pattern: "oyunhaberleri/{action=Index}/{id?}", defaults: new { controller = "News" });
            applicationBuilder.MapControllerRoute(name: "Review", pattern: "incelemeler/{action=Index}/{id?}", defaults: new { controller = "Review" });
            applicationBuilder.MapControllerRoute(name: "Document", pattern: "dosya/{action=Index}/{id?}", defaults: new { controller = "Document" });
            applicationBuilder.MapControllerRoute(name: "TV", pattern: "tv/{action=Index}/{id?}", defaults: new { controller = "TV" });
            applicationBuilder.MapControllerRoute(name: "Interview", pattern: "roportaj/{action=Index}/{id?}", defaults: new { controller = "Interview" });
            applicationBuilder.MapControllerRoute(name: "Hardware", pattern: "donanim/{action=Index}/{id?}", defaults: new { controller = "Hardware" });
            applicationBuilder.MapControllerRoute(name: "GameGuide", pattern: "rehber/{action=Index}/{id?}", defaults: new { controller = "GameGuide" });
            applicationBuilder.MapControllerRoute(name: "Magazine", pattern: "dergi/{action=Index}/{id?}", defaults: new { controller = "Magazine" });
            applicationBuilder.MapControllerRoute(name: "Home", pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
