using System.ComponentModel.DataAnnotations;

namespace MaintenanceServiceMVC.ViewModels
{
    public class EditPasswordVM
    {
        public string UserId { get; set; } = "";
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6)]
        public string NewPassword { get; set; } = "";
    }
}
