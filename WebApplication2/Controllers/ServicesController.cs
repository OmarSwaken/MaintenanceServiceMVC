//using MaintenanceServiceMVC.Data;
using MaintenanceServiceMVC.Models;
using MaintenanceServiceMVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceServiceMVC.Controllers
{
    public class ServicesController : Controller
    {
        

        private readonly IRepository<Service> _serviceRepository;

        public ServicesController(IRepository<Service> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [AllowAnonymous]
        public async Task <IActionResult> Index()
        {
            // Show available services in cards.
            var services = await _serviceRepository.GetAllAsync();
            return View(services);
        }

        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> AdminIndex()
        {
            // Show all services in a table with options to create, edit, delete.
            var services = await _serviceRepository.GetAllAsync();
            return View(services);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            // Show form to create a new service.
            return View();
        }

        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(Service service)
        {
            var existing = (await _serviceRepository.FindAsync(s => s.Name == service.Name)).FirstOrDefault();
            if (existing != null)
            {
                ModelState.AddModelError("Name", "Service name must be unique.");
            }
            
            if (ModelState.IsValid)
            {
                await _serviceRepository.AddAsync(service);
                await _serviceRepository.SaveChangesAsync();
                return RedirectToAction("AdminIndex");
            }
            return View(service);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null) return NotFound();
            return View(service);
        }

        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(Service model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = await _serviceRepository.GetByIdAsync(model.ServiceId);
            if (service == null) return NotFound();

            service.Name = model.Name;
            service.Description = model.Description;

            _serviceRepository.Update(service);
            await _serviceRepository.SaveChangesAsync();

            return RedirectToAction("AdminIndex");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null) return NotFound();

            _serviceRepository.Delete(service);
            await _serviceRepository.SaveChangesAsync();

            var services = await _serviceRepository.GetAllAsync();
            return PartialView("_ServiceTable", services);
        }
        [Authorize(Roles = "Customer")]
        public IActionResult ChooseProfessionalService(int serviceId)
        {
            
            //Show list of professionals' services from Model(ProfessionalService) filtered by service, sortable by price/rate, searchable by name.
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null) return NotFound();
            return View(service);
        }



    }
}
