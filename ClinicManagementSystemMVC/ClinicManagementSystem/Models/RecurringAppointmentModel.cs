namespace _2026_EMS_Project_new_Batch.Models
{
    public class RecurringAppointmentModel               //For Paient to book recurring appointments with a doctor (e.g., for regular check-ups or therapy sessions)
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Frequency { get; set; } // e.g., "Weekly", "Monthly"
        public string Notes { get; set; }
    }
}
