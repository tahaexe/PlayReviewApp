using Microsoft.AspNetCore.Mvc;
using PlayReviewApp.PlayReviewDb.Business.Abstract;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Components
{
    public class NewsSummaryViewComponent : ViewComponent
    {
        private readonly IServiceManager _serviceManager;

        public NewsSummaryViewComponent(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public string Invoke()
        {
            return _serviceManager.NewsManager.GetAll().Count().ToString();
        }
    }
}
