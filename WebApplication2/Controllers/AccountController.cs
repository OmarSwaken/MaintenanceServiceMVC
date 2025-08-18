using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MaintenanceServiceMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Profile()
        {
            //[Authorize] (any authenticated user)
            //Show / edit profile info(name, phone, email, reset password, some read-only fields like role).
            return View();
        }

        


    }
}
