namespace ClinicManagementSystem.Models
{
    public class BillViewModel
    {
        public int BillId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }

        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime AppointmentDate { get; set; }

        public string BillType { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BillDate { get; set; }
        public bool Paid { get; set; }
    }
}
