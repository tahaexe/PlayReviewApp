using PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.ConfigureIdentity();
builder.Services.ConfigureSession();
builder.Services.ConfigureApplicationCookie();

builder.Services.ConfigureServiceRegistration();
builder.Services.ConfigureRepositoryRegistration();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Dasboard}/{action=index}/{id?}");

app.MapControllerRoute(
    name: "Home",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.ConfigureMigration();
app.ConfigureLocalization();
app.ConfigureDefaultAdminUser();
app.UseStatusCodePagesWithReExecute("/Account/AccessDenied");

app.Run();
