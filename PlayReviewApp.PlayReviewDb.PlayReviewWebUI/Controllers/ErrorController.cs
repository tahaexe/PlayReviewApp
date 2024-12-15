using Microsoft.AspNetCore.Mvc;
using PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Models;
using System.Diagnostics;
using System.Net;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error(int code)
        {
            var viewModel = new ErrorViewModel
            {
                StatusCode = code,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}
