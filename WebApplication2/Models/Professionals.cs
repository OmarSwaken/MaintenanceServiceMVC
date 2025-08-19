using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaintenanceServiceMVC.Models
{
    public class Professional
    {
        public int ProfessionalId { get; set; }
        public bool IsAvailable { get; set; } = true;


        [Range(0, 5)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Rating { get; set; } = 0;


        // FK to Identity User
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        //New
        [Required(ErrorMessage = "Hourly rate is required")]
        [Range(0, 10000, ErrorMessage = "Hourly rate must be a positive value")]
        [DataType(DataType.Currency)]
        public decimal HourlyRate { get; set; }

        public int ServiceId { get; set; }

        public Service? Service { get; set; }

        [ValidateNever]
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        [ValidateNever]
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

        /////////////
        //public DateTime JoinDate { get; set; } = DateTime.Now;

        //[Required]
        //[StringLength(100)]
        //public string Name { get; set; } = string.Empty;

        //[Required]
        //[Phone]
        //[StringLength(25)]
        //public string Phone { get; set; } = string.Empty;

        //[Required]
        //[EmailAddress]
        //[StringLength(254)]
        //public string Email { get; set; } = string.Empty;



        //[Range(0, 60)]
        //public int ExperienceYears { get; set; }

        //public ICollection<ProfessionalService> ProfessionalServices { get; set; } = new List<ProfessionalService>();

        //[Required]
        //[StringLength(100)]
        //public string Specialty { get; set; } = string.Empty;
    }
}
