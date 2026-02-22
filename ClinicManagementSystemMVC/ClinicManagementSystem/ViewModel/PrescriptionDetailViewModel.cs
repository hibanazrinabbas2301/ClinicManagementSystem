namespace ClinicManagementSystem.ViewModel
{
    public class PrescriptionDetailViewModel
    {
        public int PrescriptionId { get; set; }

        public int MedicineId { get; set; }   // ✅ ADD THIS

        public string MedicineName { get; set; }

        public int Quantity { get; set; }

        public int Frequency { get; set; }

        public int DurationDays { get; set; }
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Dosage { get; set; }
    }

}
