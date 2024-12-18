using Microsoft.AspNetCore.Mvc;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
