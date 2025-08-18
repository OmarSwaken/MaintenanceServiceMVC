using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaintenanceServiceMVC.Models
{
    public class Professional
    {
        public int ProfessionalId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Specialty { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(25)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(254)]
        public string Email { get; set; } = string.Empty;

        [Range(0, 5)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Rating { get; set; } = 0;

        [Range(0, 60)]
        public int ExperienceYears { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal HourlyRate { get; set; }

        public bool IsAvailable { get; set; } = true;

        public DateTime JoinDate { get; set; } = DateTime.Now;

        // FK to Identity User
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        [ValidateNever]
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        [ValidateNever]
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        [ValidateNever]
        public ICollection<ProfessionalService> ProfessionalServices { get; set; } = new List<ProfessionalService>();
    }
}
