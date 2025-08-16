using Microsoft.EntityFrameworkCore;

namespace MaintenanceServiceMVC.Models
{
    public class ProfessionalService
    {
        public int ProfessionalId { get; set; }
        public int ServiceId { get; set; }

        public bool IsCertified { get; set; } = false;
        public DateTime CertificationDate { get; set; } = DateTime.Now;
        public Professional Professional { get; set; } = null!;
        public Service Service { get; set; } = null!;

        
    }
}
