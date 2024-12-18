using Microsoft.AspNetCore.Mvc;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Controllers
{
    public class GameGuideController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
