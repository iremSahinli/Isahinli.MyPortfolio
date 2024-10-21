using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Areas.Admin.Models;

namespace MyPortfolio.Areas.Admin.Controllers
{
    public class AccountController : AdminBaseController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);  // Kullanıcıyı e-posta ile bul
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz Giriş");
                return View(model);
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin)
            {
                ModelState.AddModelError(string.Empty, "Admin değilsiniz, giriş yapamazsınız!");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Hesap kilitli!");

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Başarısız deneme, tekrar deneyiniz!");
                return View(model);

            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });

        }
    }
}
