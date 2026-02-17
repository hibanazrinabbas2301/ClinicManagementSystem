namespace ClinicManagementSystem.ViewModel
{
    public class PrescriptionDetailViewModel
    {
        public int PrescriptionId { get; set; }

        public int MedicineId { get; set; }   // ✅ ADD THIS

        public string MedicineName { get; set; }

        public int Quantity { get; set; }

        public string Frequency { get; set; }

        public int DurationDays { get; set; }
    }

}
