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
    }
}
