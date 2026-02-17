namespace ClinicManagementSystem.ViewModel
{
    public class UpdateMedicineViewModel
    {


            public int MedicineId { get; set; }

            public string MedicineName { get; set; }

            public string MedicineDescription { get; set; }

            public int CategoryId { get; set; }

            public int AddedQuantity { get; set; } = 0;

            public decimal? NewPrice { get; set; }

            public DateTime? NewExpiry { get; set; }


    }
}
