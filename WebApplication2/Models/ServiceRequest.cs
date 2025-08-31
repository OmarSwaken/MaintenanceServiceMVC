using MaintenanceServiceMVC.Models.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaintenanceServiceMVC.Models
{
    public class ServiceRequest
    {
        public int ServiceRequestId { get; set; }

        [Required]
        public int CustomerId { get; set; }


        [Required]
        public int ProfessionalId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        public DateTime RequestDate { get; set; } = DateTime.Now;

        [FutureDate(ErrorMessage = "Scheduled date must be in the future.")]
        public DateTime? ScheduledDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        [Range(0, double.MaxValue)]

        [Column(TypeName = "decimal(18,2)")]
        public decimal? FinalPrice { get; set; }

        //هنحط هنا حالة الطلب pending,assigned,inprogress,completed,canceled
        [StringLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Assigned, InProgress, Completed, Cancelled

        [StringLength(1000)]
        public string? Notes { get; set; }

        
        public Customer? Customer { get; set; } = null!;
        public Professional? Professional { get; set; }
        public Service? Service { get; set; } = null!;
    }
}
