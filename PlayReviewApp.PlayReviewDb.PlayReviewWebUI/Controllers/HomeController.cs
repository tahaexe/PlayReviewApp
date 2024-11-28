using Microsoft.AspNetCore.Mvc;
using PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Models;
using PlayReviewApp.PlayReviewDb.Business.Abstract;
using System.Diagnostics;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
