using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.ViewModel
{
    public class AddMedicineViewModel :IValidatableObject
    {
        [Required(ErrorMessage = "Medicine name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string MedicineName { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        // ✅ Stock cannot be negative
        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Expiry date is required")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            if (ExpiryDate.Date < DateTime.Today)
            {
                yield return new ValidationResult(
                    "Expiry date cannot be in the past",
                    new[] { nameof(ExpiryDate) });
            }
        }

        // ✅ Price cannot be negative or zero
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 9999999, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
    }
}