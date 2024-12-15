using Microsoft.AspNetCore.Mvc.Rendering;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Infrastructure.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string ImagePath(this IHtmlHelper helper, string fileName)
        {
            return $"~/wwwroot/images/{fileName}";
        }
    }

}
