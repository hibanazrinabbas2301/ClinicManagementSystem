using System.ComponentModel.DataAnnotations;
namespace ClinicManagementSystem.View_Model
{

    public class UserModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 4,
            ErrorMessage = "Password must be between 4 and 20 characters")]
        public string UserPassword { get; set; }
    }
}
