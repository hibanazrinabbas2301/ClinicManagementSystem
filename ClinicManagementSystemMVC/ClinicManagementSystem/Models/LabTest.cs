using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models
{
    public class LabTest
    {
        public int TestId { get; set; }

        [Required]
        [StringLength(100)]
        public string TestName { get; set; }

        [Required]
        [Range(1, 100000)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        public string SampleType { get; set; }

        [StringLength(100)]
        public string? NormalRange { get; set; }
    }
}
