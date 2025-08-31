using System.ComponentModel.DataAnnotations;

namespace MaintenanceServiceMVC.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [StringLength(200)]
        [Required]
        public string Address { get; set; } = string.Empty;


        // FK to Identity User
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

      
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        /// <summary>
        /// ////////////////////
        /// </summary>
        //[Required]
        //[StringLength(100)]
        //public string Name { get; set; } = string.Empty;

        //[Required]
        //[EmailAddress]
        //[StringLength(254)]
        //public string Email { get; set; } = string.Empty;

        //[Required]
        //[Phone]
        //[StringLength(25)]
        //public string Phone { get; set; } = string.Empty;

    }
}
