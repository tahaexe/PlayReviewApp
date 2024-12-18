using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayReviewApp.PlayReviewDb.Business.Abstract;
using PlayReviewApp.PlayReviewDb.Entities.Concrete;
using PlayReviewApp.PlayReviewDb.Entities.DTO.News;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public CategoryController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [Area("Admin")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Index()
        {
            var categories = _serviceManager.CategoryManager.GetAll();
            return View(categories);
        }

        [Area("Admin")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] Category category)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.CategoryManager.Add(category);

                TempData["ToastMessage"] = "success: Güncelleme işlemi başarılı!";

                return RedirectToAction("Index", "Category");
            }

            if (ModelState.Values.Count() > 0)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ToastMessage"] = "error: " + string.Join(" ", errorMessages);
            }

            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update([FromRoute(Name = "id")] int categoryId)
        {
            var category = _serviceManager.CategoryManager.GetById(categoryId);
            return View(category);
        }

        [Area("Admin")]
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([FromForm] Category category)
        {
            if (ModelState.IsValid)
            {
                _serviceManager.CategoryManager.Update(category);

                TempData["ToastMessage"] = "success: Güncelleme işlemi başarılı!";

                return RedirectToAction("Index", "Category");
            }

            if (ModelState.Values.Count() > 0)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["ToastMessage"] = "error: " + string.Join(" ", errorMessages);
            }

            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete([FromRoute(Name = "id")] int categoryId)
        {
            _serviceManager.CategoryManager.Delete(categoryId);

            TempData["ToastMessage"] = "success: Silme işlemi başarılı!";

            return RedirectToAction("Index", "Category");
        }
    }
}
