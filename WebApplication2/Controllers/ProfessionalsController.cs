using Microsoft.AspNetCore.Mvc;

namespace MaintenanceServiceMVC.Controllers
{
    public class ProfessionalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
