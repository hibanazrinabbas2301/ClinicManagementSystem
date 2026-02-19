namespace ClinicManagementSystem.Models
{
    public class LabTestPrescription
    {
        public int PrescriptionId { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public int? TestId { get; set; }
        public string TestName { get; set; }
        public string SampleType { get; set; }
        public decimal? Price { get; set; }
        public string NormalRange { get; set; }
        public int Quantity { get; set; }
        public DateTime PrescribedDate { get; set; }
        public string Status { get; set; }

        // ✅ ADD THIS
        public int? ResultId { get; set; }
    }

}