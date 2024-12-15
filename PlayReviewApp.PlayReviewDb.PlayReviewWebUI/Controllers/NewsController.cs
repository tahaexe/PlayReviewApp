using Microsoft.AspNetCore.Mvc;
using PlayReviewApp.PlayReviewDb.Business.Abstract;
using X.PagedList.Extensions;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Controllers
{
    public class NewsController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public NewsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IActionResult Index(int page = 1)
        {
            var news = _serviceManager.NewsManager.GetAll().ToPagedList(page, 9);

            return View(news);
        }

        public IActionResult Get([FromRoute(Name = "id")] int id)
        {
            var selectedNews = _serviceManager.NewsManager.GetById(id);

            return View(selectedNews);
        }
    }
}
