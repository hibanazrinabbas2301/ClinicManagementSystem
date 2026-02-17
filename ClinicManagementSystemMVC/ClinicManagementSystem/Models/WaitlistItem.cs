namespace _2026_EMS_Project_new_Batch.Models
{
    public class WaitlistItem
    {
        public int WaitlistId { get; set; }
        public int PatientId { get; set; }
        public DateTime AddedDate { get; set; }
        public string Status { get; set; }  // e.g. "Waiting", "Called", "Removed"
    }
}
