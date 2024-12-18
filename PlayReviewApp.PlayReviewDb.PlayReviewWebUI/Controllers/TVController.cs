using Microsoft.AspNetCore.Mvc;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Controllers
{
    public class TVController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
