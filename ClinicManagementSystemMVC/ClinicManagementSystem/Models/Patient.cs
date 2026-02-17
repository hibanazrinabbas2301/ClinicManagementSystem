namespace ClinicManagementSystem.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string ContactNumber { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }
        public string MMRNo { get; set; }
        public int DoctorId { get; set; }
        public string BloodGroup { get; set; }
        public string Status { get; set; } = "Active"; // default value
    }
}
