using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class LabResultViewModel
    {
        public int PrescriptionId { get; set; }
        public int TestId { get; set; }
        public int PatientId { get; set; }
        public string NormalRange { get; set; }            // Single field
        [Required] public decimal ActualValue { get; set; }
        public string? Remarks { get; set; }
    }
}
