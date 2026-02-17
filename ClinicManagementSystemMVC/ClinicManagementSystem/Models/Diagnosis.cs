namespace ClinicManagementSystem.Models
{
    public class Diagnosis
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public string? Symptoms { get; set; }

        // ✅ Used for Insert (Diagnosis Form)
        public string? DiagnosisText { get; set; }

        public string? DoctorNotes { get; set; }

        // ✅ Extra Fields for History Display
        public DateTime Date { get; set; }

        public string? DiagnosisName { get; set; }
    }
}
