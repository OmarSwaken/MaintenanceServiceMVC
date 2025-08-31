using MaintenanceServiceMVC.Models;
using MaintenanceServiceMVC.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceServiceMVC.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Professional> _professionalRepository;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IRepository<ServiceRequest> _requestRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminApiController(
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

        
        [HttpGet("dashboard")]
        public async Task<ActionResult<AdminDashboardViewModel>> GetDashboard()
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

            return Ok(model);
        }

        
        [HttpGet("users")]
        public ActionResult<IEnumerable<ApplicationUser>> GetUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        
        [HttpGet("services")]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            var services = await _serviceRepository.GetAllAsync();
            return Ok(services);
        }

    }
}
