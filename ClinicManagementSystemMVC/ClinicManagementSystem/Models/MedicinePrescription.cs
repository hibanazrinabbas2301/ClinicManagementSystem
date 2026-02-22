namespace ClinicManagementSystem.Models
{
    public class MedicinePrescription
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public int MedicineId { get; set; }
        public int Quantity { get; set; }

        public int Frequency { get; set; }

        public string Dosage { get; set; }

        public int DurationDays { get; set; }
        public string MedicineName { get; set; }
        public string PatientName { get; set; }

    }
}
