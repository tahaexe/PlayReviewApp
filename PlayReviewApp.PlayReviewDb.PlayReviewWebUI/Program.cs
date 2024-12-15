using PlayReviewApp.PlayReviewDb.Business.Mappings.AutoMapper.Profiles;
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
builder.Services.ConfigureRouting();
builder.Services.AddAutoMapper(typeof(BusinessProfile));

var app = builder.Build();

app.ConfigureErrorCodes(app.Environment);
app.UseStaticFiles();
app.UseSession();

app.UseHttpsRedirection();
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

app.MapControllers();
app.MapRazorPages();

app.ConfigureMigration();
app.ConfigureLocalization();
app.ConfigureDefaultAdminUser();


app.Run();
