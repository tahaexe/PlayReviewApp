using Microsoft.AspNetCore.Identity;
using PlayReviewApp.PlayReviewDb.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Business.Concrete
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<INewsService> _newsService;
        private readonly Lazy<IAuthService> _authService;

        public ServiceManager(ICategoryService categoryService, INewsService newsService, IAuthService authService)
        {
            _categoryService = new Lazy<ICategoryService>(() => categoryService);
            _newsService = new Lazy<INewsService>(() => newsService);
            _authService = new Lazy<IAuthService>(() => authService);
        }

        public ICategoryService CategoryManager => _categoryService.Value;
        public INewsService NewsManager => _newsService.Value;
        public IAuthService AuthManager => _authService.Value;
    }
}
