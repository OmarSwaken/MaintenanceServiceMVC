using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MaintenanceServiceMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; } = DateTime.Now;

        public Customer? Customer { get; set; }
        public Professional? Professional { get; set; }
    }
}
