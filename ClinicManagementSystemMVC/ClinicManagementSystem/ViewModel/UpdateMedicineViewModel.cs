using ClinicManagementSystem.Controllers;
using ClinicManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace ClinicManagementSystem.ViewModel
{
    public class UpdateMedicineViewModel
    {

        public int MedicineId { get; set; }

        [Required(ErrorMessage = "Medicine name is required")]
        public string MedicineName { get; set; }

        public string MedicineDescription { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        // ✅ Added Stock cannot be negative
        [Range(0, int.MaxValue, ErrorMessage = "Added quantity cannot be negative")]
        public int AddedQuantity { get; set; } = 0;

        // ✅ Price cannot be negative
        [Range(0.01, 9999999, ErrorMessage = "Price must be greater than 0")]
        public decimal? NewPrice { get; set; }

        // ✅ Expiry must be future date (optional)
        [DataType(DataType.Date)]
        public DateTime? NewExpiry { get; set; }
    }
}

