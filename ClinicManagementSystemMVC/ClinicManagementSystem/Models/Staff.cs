using System.Data;

namespace ClinicManagementSystem.Models
{
    public class Staff
    {
        public int StaffId { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int? RoleId { get; set; }
        public string Status  { get; set; } 
        public virtual Roles? Role { get; set; }
    }
}
