using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class LabResultViewModel
    {
        public int PrescriptionId { get; set; }
        public int TestId { get; set; }
        public int PatientId { get; set; }

        [Required]
        public decimal LowRange { get; set; }

        [Required]
        public decimal HighRange { get; set; }

        [Required]
        public decimal ActualValue { get; set; }

        public string? Remarks { get; set; }
    }
}
