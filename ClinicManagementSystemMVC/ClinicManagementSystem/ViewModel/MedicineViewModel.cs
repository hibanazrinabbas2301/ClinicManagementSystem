namespace ClinicManagementSystem.ViewModel
{
    public class MedicineViewModel
    {
        public int MedicineId { get; set; }

        public string MedicineName { get; set; }

        public string MedicineDescription { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }


        public int CurrentStock { get; set; }

        public DateTime ExpiryDate { get; set; }

        public decimal Price { get; set; }
    }
}
