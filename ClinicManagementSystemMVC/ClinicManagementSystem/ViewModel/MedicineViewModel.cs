using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.ViewModel
{
    public class MedicineViewModel
    {
        public int MedicineId { get; set; }

        public string MedicineName { get; set; }

        public string MedicineDescription { get; set; }

        public int CategoryId { get; set; }

        // 🔥 ADD THESE (Missing Properties For Listing)

        public string CategoryName { get; set; }

        public int CurrentStock { get; set; }

        public decimal Price { get; set; }

        public DateTime? ExpiryDate { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            if (NewExpiry.HasValue && NewExpiry.Value.Date < DateTime.Today)
            {
                yield return new ValidationResult(
                    "Expiry date cannot be in the past",
                    new[] { nameof(NewExpiry) });
            }
        }

        // ------------------------------------------
        // For Update Logic
        // ------------------------------------------

        [Range(0, int.MaxValue, ErrorMessage = "Added stock cannot be negative")]
        public int AddedQuantity { get; set; } = 0;

        [Range(0.01, 9999999, ErrorMessage = "Price must be greater than 0")]
        public decimal? NewPrice { get; set; }

        [DataType(DataType.Date)]
        public DateTime? NewExpiry { get; set; }
    }
}