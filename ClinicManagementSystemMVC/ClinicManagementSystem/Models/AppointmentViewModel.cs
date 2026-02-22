namespace ClinicManagementSystem.Models
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }
        public string PatientName { get; set; }

        public int DoctorId { get; set; }
        public string DoctorName { get; set; }

        // ✅ REQUIRED (from your SP)
        public int SlotId { get; set; }
        public DateTime SlotDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public decimal ConsultationBill { get; set; }

        public string Phone { get; set; }

        public int? TokenNumber { get; set; }
        public string Status { get; set; }

        // Optional (keep if you use them somewhere)
        public DateTime AppointmentDate { get; set; }
        public string Gender { get; set; }
        public string MMRNo { get; set; }
        public int? Age { get; set; }
    }
}