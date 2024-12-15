using Microsoft.AspNetCore.Mvc;
using PlayReviewApp.PlayReviewDb.Business.Abstract;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Components
{
    public class NewsCarouselViewComponent : ViewComponent
    {
        private IServiceManager _serviceManager;

        public NewsCarouselViewComponent(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IViewComponentResult Invoke()
        {
            var lastNews = _serviceManager.NewsManager.GetTopNews(5);

            return View(lastNews);
        }
    }
}
