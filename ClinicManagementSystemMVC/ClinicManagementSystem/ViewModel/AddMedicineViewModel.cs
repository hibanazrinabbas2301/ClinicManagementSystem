using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.ViewModel
{
    public class AddMedicineViewModel
    {
        [Required(ErrorMessage = "Medicine name is required")]
        [StringLength(100)]
        public string MedicineName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be valid")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Expiry date is required")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 9999999, ErrorMessage = "Price must be valid")]
        public decimal Price { get; set; }
    }
}
