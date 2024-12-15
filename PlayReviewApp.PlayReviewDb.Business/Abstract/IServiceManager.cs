using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayReviewApp.PlayReviewDb.Business.Abstract
{
    public interface IServiceManager
    {
        INewsService NewsManager { get; }
        ICategoryService CategoryManager { get; }
        IAuthService AuthManager { get; }
    }
}
