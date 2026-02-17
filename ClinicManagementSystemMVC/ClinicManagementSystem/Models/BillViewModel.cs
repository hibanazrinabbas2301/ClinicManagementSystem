namespace ClinicManagementSystem_Final.Models
{
    public class BillViewModel
    {
        public string PatientName;
        public string DoctorName;
        public DateTime AppointmentDate;

        public int BillId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public string BillType { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BillDate { get; set; }
        public bool Paid { get; set; }
    }
}
