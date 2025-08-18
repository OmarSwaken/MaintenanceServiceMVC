using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MaintenanceServiceMVC.Controllers
{
    public class ProfessionalsController : Controller
    {
        public IActionResult Index()
        {
            // List of professionals (could be filtered by service type, search by name, sort by price/h or rating )
            return View();
        }

        [Authorize (Roles = "Professional")]
        public IActionResult Dashboard()
        {
            //list of assigned requests.
            //accept or reject requests.

            return View();
        }

        [Authorize (Roles = "Professional")]
        public IActionResult MyServices()
        {
            // Show services offered by the professional and allow management (view, edit, delete).
            return View();
        }
    }
}
