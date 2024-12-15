using Microsoft.AspNetCore.Mvc;
using PlayReviewApp.PlayReviewDb.Business.Abstract;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Components
{
    public class PopularGamesListViewComponent(IServiceManager serviceManager) : ViewComponent
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public IViewComponentResult Invoke(string page = "default")
        {
            var popularNews = _serviceManager.NewsManager.GetTopNews(5);

            return View(popularNews);
        }
    }
}
