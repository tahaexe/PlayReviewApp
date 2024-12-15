using Microsoft.AspNetCore.Mvc;
using PlayReviewApp.PlayReviewDb.Business.Abstract;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Components
{
    public class UserSummaryViewComponent : ViewComponent
    {
        private IServiceManager _serviceManager;

        public UserSummaryViewComponent(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public string Invoke()
        {
            return _serviceManager.AuthManager.UserManager.Users.Count().ToString();
        }
    }
}
