using Microsoft.AspNetCore.Identity;

namespace MaintenanceServiceMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FullName { get; set; }


        public Customer? Customer { get; set; }
        public Professional? Professional { get; set; }
    }
}
