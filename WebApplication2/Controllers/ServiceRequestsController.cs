using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Customer")]
        public IActionResult MyRequests()
        {
            return View();
        }

        [Authorize(Roles = "Professional")]
        public IActionResult AssignedRequests()
        {
            // Only professionals can access
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
