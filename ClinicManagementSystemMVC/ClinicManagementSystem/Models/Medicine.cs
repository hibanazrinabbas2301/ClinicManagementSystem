namespace ClinicManagementSystem.Models
{
    public class Medicine
    {



        public int MedicineId { get; set; }

        public string MedicineName { get; set; }

        public string MedicineDescription { get; set; }

        public int CategoryId { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }

        public decimal Price { get; set; }
    }
}
