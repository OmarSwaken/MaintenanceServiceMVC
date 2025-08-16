using Microsoft.AspNetCore.Mvc;

namespace MaintenanceServiceMVC.Controllers
{
    public class ReviewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int professionalId, int requestId)
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
