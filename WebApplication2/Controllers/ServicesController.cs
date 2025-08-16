using MaintenanceServiceMVC.Data;
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

        public IActionResult Index()
        {
            var services = _context.Services.ToList();
            return View(services);
        }

        public IActionResult Details(int id)
        {
            var service = _context.Services.FirstOrDefault(s => s.ServiceId == id);
            if (service == null) return NotFound();
            return View(service);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return View();
        }


    }
}
