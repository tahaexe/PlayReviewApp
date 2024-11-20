using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Business.Abstract
{
    public interface IServiceManager
    {
        INewsService NewsService { get; }
        ICategoryService CategoryService { get; }
        IUserService UserService { get; }
    }
}
