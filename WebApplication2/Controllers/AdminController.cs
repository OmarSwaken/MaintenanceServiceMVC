using MaintenanceServiceMVC.Data;
using MaintenanceServiceMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceServiceMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

        [Authorize(Roles = "Admin")]
        public IActionResult UserManagement()
        {
            // Manage users (view, edit, delete users)
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ServiceManagement()
        {
            // Manage services (view, add, edit, delete services)
            return View();
        }
    }
}
