using AutoMapper;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayReviewApp.PlayReviewDb.Business.Abstract;
using PlayReviewApp.PlayReviewDb.Entities.DTO.News;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public NewsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var allNews = _serviceManager.NewsManager.GetAll();
            return View(allNews);
        }

        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] NewsDtoForCreate newsDtoForCreate, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(newsDtoForCreate.Content))
                {
                    var sanitizer = new HtmlSanitizer();
                    //sanitizer.AllowedAttributes.Add("style");
                    newsDtoForCreate.Content = sanitizer.Sanitize(newsDtoForCreate.Content);
                }

                if (file != null && file.Length > 0)
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    var fileName = $"{Guid.NewGuid()}{fileExtension}";

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    newsDtoForCreate.ImageUrl = $"/images/{fileName}";
                }

                _serviceManager.NewsManager.Create(newsDtoForCreate);

                TempData["ToastMessage"] = "success: Güncelleme işlemi başarılı!";

                return RedirectToAction("Index", "News");
            }

            if (ModelState.Values.Count() > 0)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ToastMessage"] = "error: " + string.Join(" ", errorMessages);
            }

            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute(Name = "id")] int newsId)
        {
            var news = _serviceManager.NewsManager.GetById(newsId);

            return View(news);
        }

        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] NewsDtoForUpdate newsDtoForUpdate, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(newsDtoForUpdate.Content))
                {
                    var sanitizer = new HtmlSanitizer();
                    //sanitizer.AllowedAttributes.Add("style");
                    newsDtoForUpdate.Content = sanitizer.Sanitize(newsDtoForUpdate.Content);
                }

                if (file != null && file.Length > 0)
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    var fileName = $"{Guid.NewGuid()}{fileExtension}";

                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    newsDtoForUpdate.ImageUrl = $"/images/{fileName}";
                }

                _serviceManager.NewsManager.Update(newsDtoForUpdate);

                TempData["ToastMessage"] = "success: Güncelleme işlemi başarılı!";

                return RedirectToAction("Index", "News");
            }

            if (ModelState.Values.Count() > 0)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ToastMessage"] = "error: " + string.Join(" ", errorMessages);
            }

            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromRoute(Name = "id")] int newsId)
        {
            _serviceManager.NewsManager.Delete(newsId);

            TempData["ToastMessage"] = "success: Silme işlemi başarılı!";

            return RedirectToAction("Index", "News");
        }
    }
}
