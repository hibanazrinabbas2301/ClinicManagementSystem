namespace ClinicManagementSystem.ViewModel
{
    public class IssuedMedicineBillViewModel
    {

        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string MedicineName { get; set; }
        public int QuantityIssued { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
