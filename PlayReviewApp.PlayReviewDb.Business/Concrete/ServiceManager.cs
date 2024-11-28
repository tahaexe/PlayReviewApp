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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ServiceManager(ICategoryService categoryService, INewsService newsService, UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            _categoryService = new Lazy<ICategoryService>(() => categoryService);
            _newsService = new Lazy<INewsService>(() => newsService);
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ICategoryService CategoryService => _categoryService.Value;
        public INewsService NewsService => _newsService.Value;
        public UserManager<IdentityUser> UserManager => _userManager;
        public SignInManager<IdentityUser> SignInManager => _signInManager;
    }
}
