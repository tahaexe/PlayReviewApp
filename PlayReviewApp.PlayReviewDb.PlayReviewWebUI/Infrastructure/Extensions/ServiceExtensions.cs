using Microsoft.EntityFrameworkCore;
using PlayReviewApp.PlayReviewDb.DataAccess.Concrete.EntityFramework;
using PlayReviewApp.PlayReviewDb.Business.Abstract;
using PlayReviewApp.PlayReviewDb.Business.Concrete;
using PlayReviewApp.PlayReviewDb.DataAccess.Abstract;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PlayReviewDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("mssqlconnection"),
                b => b.MigrationsAssembly("PlayReviewWebUI"));

                options.EnableSensitiveDataLogging(true);
            });
        }

        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<INewsService, NewsManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void ConfigureRepositoryRegistration(this IServiceCollection services)
        {
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<INewsDal, EfNewsDal>();
        }
    }
}
