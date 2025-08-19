//using MaintenanceServiceMVC.Data;
using MaintenanceServiceMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MaintenanceServiceMVC.Repositories;

namespace MaintenanceServiceMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Professional> _professionalRepository;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IRepository<ServiceRequest> _requestRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(
            IRepository<ApplicationUser> userRepository,
            IRepository<Customer> customerRepository,
            IRepository<Professional> professionalRepository,
            IRepository<Service> serviceRepository,
            IRepository<ServiceRequest> requestRepository,
            UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _professionalRepository = professionalRepository;
            _serviceRepository = serviceRepository;
            _requestRepository = requestRepository;
            _userManager = userManager;
        }


        public async Task<IActionResult> Dashboard()
        {
            var totalUsers = _userManager.Users.Count();
            var totalCustomers = (await _customerRepository.GetAllAsync()).Count();
            var totalProfessionals = (await _professionalRepository.GetAllAsync()).Count();
            var totalServices = (await _serviceRepository.GetAllAsync()).Count();
            var totalRequests = (await _requestRepository.GetAllAsync()).Count();

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
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ServiceManagement()
        {
            var services = await _serviceRepository.GetAllAsync();
            return View(services);
        }

        //[Authorize(Roles = "Admin")]
        //public IActionResult UserManagement()
        //{
        //    // Manage users (view, edit, delete users)
        //    return View();
        //}

        //[Authorize(Roles = "Admin")]
        //public IActionResult ServiceManagement()
        //{
        //    // Manage services (view, add, edit, delete services)
        //    return View();
        //}
    }
}
