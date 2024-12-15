using Microsoft.AspNetCore.Mvc;
using PlayReviewApp.PlayReviewDb.Business.Abstract;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Components
{
    public class ShowcaseViewComponent : ViewComponent
    {
        private readonly IServiceManager _serviceManager;

        public ShowcaseViewComponent(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IViewComponentResult Invoke()
        {
            var allNews = _serviceManager.NewsManager.GetTopNews(15);
            return View(allNews);
            //return page.Equals("default")
            //? View(allNews)
            //: View("list", allNews);
        }
    }
}
