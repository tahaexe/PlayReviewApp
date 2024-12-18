using Microsoft.AspNetCore.Mvc;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Controllers
{
    public class MagazineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
