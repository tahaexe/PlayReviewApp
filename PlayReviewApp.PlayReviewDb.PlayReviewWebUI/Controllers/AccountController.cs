using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlayReviewApp.PlayReviewDb.Business.Abstract;
using PlayReviewApp.PlayReviewDb.Entities.DTO.Account;
using PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Models;

namespace PlayReviewApp.PlayReviewDb.PlayReviewWebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public AccountController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _serviceManager.AuthManager.UserManager.FindByNameAsync(loginModel.UserName);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Please check your username and password");
                    return View(loginModel);
                }

                await _serviceManager.AuthManager.SignInManager.SignOutAsync();
                var result = await _serviceManager.AuthManager.SignInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Your account is locked out. Please try again later.");
                    }
                    else if (result.RequiresTwoFactor)
                    {
                        ModelState.AddModelError(string.Empty, "Two-factor authentication is required.");
                    }
                    else if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError(string.Empty, "Your account is not allowed to sign in.");
                    }
                    else
                    {
                        // Varsayılan hata mesajı
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }

                    // Hata mesajını göstermek için kullanıcıyı tekrar giriş sayfasına yönlendir
                    return View(loginModel);
                }
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser newUser = new IdentityUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                // Kullanıcıyı oluştur
                var result = await _serviceManager.AuthManager.UserManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    // Kullanıcı oluşturulduysa, ROLE ekle
                    string userRole = "USER";

                    // Rolün var olup olmadığını kontrol et
                    var roleExist = await _serviceManager.AuthManager.RoleManager.RoleExistsAsync(userRole);

                    if (!roleExist)
                    {
                        // Rol yoksa, rolü oluştur
                        var createRoleResult = await _serviceManager.AuthManager.RoleManager.CreateAsync(new IdentityRole(userRole));

                        if (!createRoleResult.Succeeded)
                        {
                            // Eğer rol oluşturulamazsa, hata mesajını ekle
                            foreach (var error in createRoleResult.Errors)
                            {
                                ModelState.AddModelError("", "Rol oluşturulamadı");
                            }
                            return View(model); // Hata ile geri dön
                        }
                    }

                    // Kullanıcıyı role ekle
                    var roleResult = await _serviceManager.AuthManager.UserManager.AddToRoleAsync(newUser, userRole);

                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Login"); // Başarıyla giriş sayfasına yönlendir
                    }
                    else
                    {
                        // Kullanıcıyı role eklerken hata olursa, hata mesajı ekle
                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError("", "Rol ataması başarısız");
                        }
                    }
                }
                else
                {
                    // Kullanıcı oluşturulamazsa hata mesajlarını ekle
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", "User can't create.");
                    }
                }
            }

            // Eğer model geçerli değilse veya hata varsa, tekrar formu göster
            return View(model);
        }


        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var identityUser = await _serviceManager.AuthManager.UserManager.GetUserAsync(User);

            return View(identityUser);
        }

        public async Task<IActionResult> Logout()
        {
            await _serviceManager.AuthManager.SignInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
