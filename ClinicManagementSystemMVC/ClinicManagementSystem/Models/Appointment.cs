namespace ClinicManagementSystem.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int TokenNumber { get; set; }

        public string PatientName { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public string Status { get; set; }
    }
}
