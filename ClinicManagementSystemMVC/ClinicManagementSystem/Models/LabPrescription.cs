namespace ClinicManagementSystem.Models
{
    public class LabPrescription
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public int TestId { get; set; }
        public int Quantity { get; set; }
        public string TestName { get; set; }

        // ✅ Extra Display Property (Pending / Completed)
        public string Status { get; set; }
    }
}
