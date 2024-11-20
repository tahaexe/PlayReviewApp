using Microsoft.AspNetCore.Mvc;
using PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Models;
using PlayReviewApp.PlayReviewDb.Business.Abstract;
using System.Diagnostics;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsService _newsService;

        public HomeController(ILogger<HomeController> logger, INewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        public IActionResult Index()
        {
            var news = _newsService.GetAll();

            return View(news);
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
