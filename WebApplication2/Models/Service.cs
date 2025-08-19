using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaintenanceServiceMVC.Models
{
    public class Service
    {
        public int ServiceId { get; set; }

        [Required]
        [StringLength(100)]
        
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        public ICollection<ProfessionalService> ProfessionalServices { get; set; } = new List<ProfessionalService>();


        //Dropped fields
        //[Range(0, double.MaxValue)]
        //[Column(TypeName = "decimal(18,2)")]
        //public decimal BasePrice { get; set; }
        //public bool IsActive { get; set; } = true;
        //public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
