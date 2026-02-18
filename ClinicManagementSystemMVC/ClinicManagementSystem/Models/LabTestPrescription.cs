namespace ClinicManagementSystem.Models
{
    public class LabTestPrescription
    {
        public int PrescriptionId { get; set; }

        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public int? TestId { get; set; }

        public string TestName { get; set; }

        public int Quantity { get; set; } = 1;

        public DateTime PrescribedDate { get; set; }

        public string Status { get; set; }

    }
}
