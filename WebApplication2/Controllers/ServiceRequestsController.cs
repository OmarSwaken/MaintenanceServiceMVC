using Microsoft.AspNetCore.Mvc;

namespace MaintenanceServiceMVC.Controllers
{
    public class ServiceRequestsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int serviceId)
        {
            return View();
        }

        public IActionResult MyRequests()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Assign(int requestId)
        {
            return View();
        }

        public IActionResult ChangeStatus(int id, string status)
        {
            return View();
        }


    }
}
