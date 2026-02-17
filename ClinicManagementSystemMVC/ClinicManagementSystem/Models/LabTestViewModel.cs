using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class LabTestViewModel
    {
        public int TestId { get; set; }

        [Required(ErrorMessage = "Test Name is required")]
        public string TestName { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Sample Type is required")]
        public string SampleType { get; set; }

        public string? NormalRange { get; set; }
    }
}