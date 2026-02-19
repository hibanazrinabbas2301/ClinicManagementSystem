using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystem.Models
{
    public class LabResult
    {
        public int ResultId { get; set; }
        [Required] public int TestId { get; set; }
        public int PatientId { get; set; }
        public string NormalRange { get; set; }           // Single field
        [Required] public decimal ActualValue { get; set; }
        [StringLength(255)] public string? Remarks { get; set; }
        public string? DoctorReview { get; set; }
        public DateTime Date { get; set; }

    }
}
