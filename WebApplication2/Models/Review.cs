using System.ComponentModel.DataAnnotations;

namespace MaintenanceServiceMVC.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ProfessionalId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string? Comment { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;

        
        public Customer Customer { get; set; } = null!;
        public Professional Professional { get; set; } = null!;
    }
}
