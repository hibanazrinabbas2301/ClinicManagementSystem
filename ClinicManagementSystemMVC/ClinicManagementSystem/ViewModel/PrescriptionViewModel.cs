namespace ClinicManagementSystem.ViewModel
{
    public class PrescriptionViewModel
    {



        public int PrescriptionId { get; set; }
        public string PatientName { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public string Frequency { get; set; }
        public int DurationDays { get; set; }
        public string Status { get; set; }
        public DateTime PrescribedDate { get; set; }
    }
}
