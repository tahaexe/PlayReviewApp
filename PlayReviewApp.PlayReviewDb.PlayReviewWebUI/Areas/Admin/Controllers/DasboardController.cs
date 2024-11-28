using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayReviewApp.PlayReviewDb.Business.Abstract;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Areas.Admin.Controllers
{
    public class DasboardController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public DasboardController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var user = await _serviceManager.UserManager.GetUserAsync(User);

            return View(user);
        }
    }
}
