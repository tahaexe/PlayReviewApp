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
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<INewsService> _newsService;

        public ServiceManager(ICategoryService categoryService, IUserService userService, INewsService newsService)
        {
            _categoryService = new Lazy<ICategoryService>(() => categoryService);
            _userService = new Lazy<IUserService>(() => userService);
            _newsService = new Lazy<INewsService>(() => newsService);
        }

        public ICategoryService CategoryService => _categoryService.Value;
        public IUserService UserService => _userService.Value;
        public INewsService NewsService => _newsService.Value;
    }
}
