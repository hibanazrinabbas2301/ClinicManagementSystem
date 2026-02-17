using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.View_Model
{
    public class UserModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserPassword { get; set; }
    }
}
