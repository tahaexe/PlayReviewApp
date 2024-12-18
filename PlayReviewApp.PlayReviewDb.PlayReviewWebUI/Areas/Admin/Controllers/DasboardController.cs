using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Areas.Admin.Controllers
{
    public class DasboardController : Controller
    {
        [Area("Admin")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
