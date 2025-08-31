using MaintenanceServiceMVC.Models;
using MaintenanceServiceMVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MaintenanceServiceMVC.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly Repository<ServiceRequest> _serviceRequestRepo;
        private readonly Repository<Customer> _customerRepo;
        private readonly Repository<Professional> _professionalRepo;
        private readonly Repository<Service> _serviceRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public ServiceRequestsController(
            Repository<ServiceRequest> serviceRequestRepo,
            Repository<Customer> customerRepo,
            Repository<Professional> professionalRepo,
            Repository<Service> serviceRepo,
            UserManager<ApplicationUser> userManager)
        {
            _serviceRequestRepo = serviceRequestRepo;
            _customerRepo = customerRepo;
            _professionalRepo = professionalRepo;
            _serviceRepo = serviceRepo;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(int professionalId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var customer = (await _customerRepo.FindAsync(c => c.UserId == user.Id)).FirstOrDefault();
            if (customer == null)
                return BadRequest("Customer profile not found");

            var professional = await _professionalRepo.GetByIdWithIncludesAsync(
                professionalId,
                p => p.User,
                p => p.Service
            );

            if (professional == null)
                return BadRequest("Professional not found");

            var model = new ServiceRequest
            {
                CustomerId = customer.CustomerId,
                ProfessionalId = professional.ProfessionalId,
                ServiceId = professional.ServiceId,
                Address = customer.Address,
                RequestDate = DateTime.Now
            };

            ViewBag.ProfessionalName = professional.User?.FullName ?? "Unknown";
            ViewBag.ServiceName = professional.Service?.Name ?? "Unknown";

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(ServiceRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var customer = (await _customerRepo.FindAsync(c => c.UserId == user.Id)).FirstOrDefault();
            if (customer == null)
                return BadRequest("Customer profile not found.");

            var professional = await _professionalRepo.GetByIdWithIncludesAsync(
                request.ProfessionalId,
                p => p.User,
                p => p.Service
            );
            if (professional == null)
                return BadRequest("Professional not found.");

            request.CustomerId = customer.CustomerId;
            request.ServiceId = professional.ServiceId; 
            request.Status = "Pending";
            request.RequestDate = DateTime.Now;

            foreach (var key in ModelState.Keys)
            {
                var errors = ModelState[key].Errors;
                foreach (var error in errors)
                {






                    Console.WriteLine($"{key}: {error.ErrorMessage}");
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.ProfessionalName = professional.User?.FullName ?? "Unknown";
                ViewBag.ServiceName = professional.Service?.Name ?? "Unknown";
                return View(request);
            }

            await _serviceRequestRepo.AddAsync(request);
            await _serviceRequestRepo.SaveChangesAsync();

            return RedirectToAction("Index", "Professionals");
        }


        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> MyRequests()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var requests = await _serviceRequestRepo.GetWithIncludeAsync(
                r => r.Customer.UserId == userId,
                r => r.Service,
                r => r.Professional,
                r => r.Professional.User
            );

            var ordered = requests.OrderByDescending(r => r.RequestDate).ToList();

            return View(ordered);
        }

        [Authorize(Roles = "Professional")]
        [HttpGet]
        public async Task<IActionResult> AssignedRequests()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var requests = await _serviceRequestRepo.GetWithIncludeAsync(
                r => r.Professional.UserId == userId,
                r => r.Customer,
                r => r.Customer.User
            );

            var ordered = requests
                .OrderByDescending(r => r.RequestDate)
                .ToList();

            return View(ordered);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            var request = await _serviceRequestRepo.GetByIdAsync(id);
            if (request == null) return NotFound();

            request.Status = status;
            _serviceRequestRepo.Update(request);
            await _serviceRequestRepo.SaveChangesAsync();

            return RedirectToAction(nameof(AssignedRequests));
        }

    }
}
