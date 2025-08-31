using MaintenanceServiceMVC.Data;
using MaintenanceServiceMVC.Models;
using MaintenanceServiceMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceServiceMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AdminController(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public async Task<IActionResult> Dashboard()
        {
            var totalUsers = _userManager.Users.Count();
            var totalCustomers = _context.Customers.Count();
            var totalProfessionals = _context.Professionals.Count();
            var totalServices = _context.Services.Count();
            var totalRequests = _context.ServiceRequests.Count();

            var model = new AdminDashboardViewModel
            {
                TotalUsers = totalUsers,
                TotalCustomers = totalCustomers,
                TotalProfessionals = totalProfessionals,
                TotalServices = totalServices,
                TotalRequests = totalRequests
            };

            return View(model);
        }

        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();

            var userRoles = new List<UserWithRolesVM>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles.Add(new UserWithRolesVM
                {
                    UserId = user.Id,
                    Email = user.Email ?? "",
                    Roles = roles.ToList()
                });
            }

            return View(userRoles);
        }

        // GET: Edit Password
        public async Task<IActionResult> EditPassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(new EditPasswordVM { UserId = user.Id, Email = user.Email ?? "" });
        }

        // POST: Edit Password
        [HttpPost]
        public async Task<IActionResult> EditPassword(EditPasswordVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            // Remove old password if exists
            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (hasPassword)
            {
                await _userManager.RemovePasswordAsync(user);
            }

            var result = await _userManager.AddPasswordAsync(user, model.NewPassword);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Users));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}
