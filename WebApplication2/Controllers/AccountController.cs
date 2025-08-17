using MaintenanceServiceMVC.Models;
using MaintenanceServiceMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceServiceMVC.Controllers
{
    public class AccountController : Controller
    {
        //or use Identity scaffold
        private readonly AuthService _authService;

        public AccountController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.Name };
                var result = await _authService.RegisterAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _authService.LoginAsync(model.Email, model.Password, false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _authService.LoginAsync(model.Email, model.Password, model.RememberMe);

                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");

                    // Show detailed reasons for debugging
                    if (result.IsLockedOut)
                        ModelState.AddModelError("", "User account locked out.");
                    else if (result.IsNotAllowed)
                        ModelState.AddModelError("", "User not allowed to log in.");
                    else if (result.RequiresTwoFactor)
                        ModelState.AddModelError("", "Two-factor authentication required.");
                    else
                        ModelState.AddModelError("", "Invalid login attempt.");
                }
                catch (Exception ex)
                {
                    // Show error (⚠️ in production use logging instead of showing details)
                    ModelState.AddModelError("", $"Login error: {ex.Message}");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
