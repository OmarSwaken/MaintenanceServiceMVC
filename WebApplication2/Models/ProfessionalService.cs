using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MaintenanceServiceMVC.Models
{
    public class ProfessionalService
    {
        public int ProfessionalId { get; set; }

        public int ServiceId { get; set; }


        public Professional Professional { get; set; } = null!;
        public Service Service { get; set; } = null!;

        //two new fields
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hourly rate is required")]
        [Range(0, 10000, ErrorMessage = "Hourly rate must be a positive value")]
        [DataType(DataType.Currency)]
        public decimal HourlyRate { get; set; }

        //Dropped fields
        //public bool IsCertified { get; set; } = false;
        //public DateTime CertificationDate { get; set; } = DateTime.Now;

    }
}
