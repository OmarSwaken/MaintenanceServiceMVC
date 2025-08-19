using MaintenanceServiceMVC.Data;
using MaintenanceServiceMVC.Models;
using MaintenanceServiceMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MaintenanceServiceMVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            // Try to load customer
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            // Try to load professional
            var professional = await _context.Professionals
                .FirstOrDefaultAsync(p => p.UserId == user.Id);

            var model = new ProfileViewModel
            {
                
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                CustomerAddress = customer?.Address,        // only if customer
            };

            return View(model);

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Profile(ProfileViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null) return RedirectToAction("Login", "Account");

        //    user.UserName = model.Username;
        //    user.Email = model.Email;
        //    user.PhoneNumber = model.PhoneNumber;

        //    var result = await _userManager.UpdateAsync(user);
        //    if (result.Succeeded)
        //    {
        //        await _signInManager.RefreshSignInAsync(user);
        //        ViewBag.Message = "Profile updated successfully!";
        //        return View(model);
        //    }

        //    foreach (var error in result.Errors)
        //        ModelState.AddModelError("", error.Description);

        //    return View(model);
        //}
    }

}







