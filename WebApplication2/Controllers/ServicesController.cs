using MaintenanceServiceMVC.Data;
using MaintenanceServiceMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaintenanceServiceMVC.Controllers
{
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            // Show available services in cards.
            var services = _context.Services.ToList();
            return View(services);
        }

        [Authorize (Roles = "Admin")]
        public IActionResult AdminIndex()
        {
            // Show all services in a table with options to add, edit, delete.
            var services = _context.Services.ToList();
            
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
        public IActionResult Create(Service service)
        {
            if (_context.Services.Any(s => s.Name == service.Name))
            {
                ModelState.AddModelError("Name", "Service name must be unique.");
            }

            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                _context.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            // If model state is invalid, return to the view with the current service data.

            return View(service);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.ServiceId == id);
            if (service == null) return NotFound();
            return View(service);
        }

        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Service model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = _context.Services.Find(model.ServiceId);
            if (service == null)
                return NotFound();

            service.Name = model.Name;
            service.Description = model.Description;
            _context.SaveChanges();

            return RedirectToAction("AdminIndex");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null) return NotFound();

            _context.Services.Remove(service);
            _context.SaveChanges();

            // Return the updated partial view
            var services = _context.Services.ToList();
            return PartialView("_ServiceTable", services);
        }

        [Authorize(Roles = "Customer")]
        public IActionResult ChooseProfessionalService(int serviceId)
        {
            //Show list of professionals' services from Model(ProfessionalService) filtered by service, sortable by price/rate, searchable by name.

            return View();
        }

        public IActionResult Details(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.ServiceId == id);
            if (service == null) return NotFound();
            return View(service);
        }



    }
}
