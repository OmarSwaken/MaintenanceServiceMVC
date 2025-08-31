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
        [Required]
        public string Description { get; set; } = string.Empty;


        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        public ICollection<Professional> Professionals { get; set; } = new List<Professional>();


        //Dropped fields
        //[Range(0, double.MaxValue)]
        //[Column(TypeName = "decimal(18,2)")]
        //public decimal BasePrice { get; set; }
        //public bool IsActive { get; set; } = true;
        //public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
