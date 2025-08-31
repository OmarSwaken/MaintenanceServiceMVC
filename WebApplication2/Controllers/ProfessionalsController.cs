using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MaintenanceServiceMVC.Data;
using Microsoft.AspNetCore.Identity;
using MaintenanceServiceMVC.Models;

namespace MaintenanceServiceMVC.Controllers
{
    public class ProfessionalsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfessionalsController(AppDbContext context, UserManager<ApplicationUser> userManeger)
        {

            _context = context;
            _userManager = userManeger;
        }

        [Authorize]
        public IActionResult Index(int? serviceId, string searchTerm, string sortBy = "name")
        {
            var query = _context.Professionals
                .Include(p => p.Service)
                .AsQueryable();

            // Filter by service if provided
            if (serviceId.HasValue)
            {
                query = query.Where(p => p.ServiceId == serviceId.Value);
            }

            // Search by professional name (from linked ApplicationUser)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Include(p => p.User)
                             .Where(p => p.User.FullName.Contains(searchTerm));
            }

            
            switch (sortBy.ToLower())
            {
                case "price":
                    query = query.OrderBy(p => p.HourlyRate);
                    break;
                case "rating":
                    query = query.OrderByDescending(p => p.Rating);
                    break;
                default:
                    query = query.Include(p => p.User)
                                 .OrderBy(p => p.User.FullName);
                    break;
            }

            var professionals = query.ToList();

            
            ViewBag.ServiceId = serviceId;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.SortBy = sortBy;
            ViewBag.Services = _context.Services.ToList();

            return View(professionals);
        }


    }
}
