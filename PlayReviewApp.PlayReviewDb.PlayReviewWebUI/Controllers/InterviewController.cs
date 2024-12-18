using Microsoft.AspNetCore.Mvc;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Controllers
{
    public class InterviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
