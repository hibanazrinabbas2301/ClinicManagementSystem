using ClassLibraryDataBaseConnection;
using ClinicManagementSystem.ViewModel;
using Microsoft.Data.SqlClient;
using System.Data;


namespace ClinicManagementSystem.Repository
{
    public class PharmacistRepoImpl:IPharmacistRepo
    {



        private readonly IConfiguration _configuration;

        // DI
        public PharmacistRepoImpl(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #region list medcine 
        public IEnumerable<MedicineViewModel> GetAllMedicines()
        {
            List<MedicineViewModel> medicines = new List<MedicineViewModel>();

            string connectionString = _configuration.GetConnectionString("ConnStringMVC");


            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                if (connection != null)
                {
                    SqlCommand cmd = new SqlCommand("sp_GetAllMedicines", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MedicineViewModel medicine = new MedicineViewModel();

                            medicine.MedicineId = Convert.ToInt32(reader["MedicineId"].ToString());
                            medicine.MedicineName = reader["MedicineName"].ToString();
                            medicine.MedicineDescription = reader["MedicineDescription"].ToString();
                            medicine.CategoryName = reader["CategoryName"].ToString();
                            medicine.CurrentStock = Convert.ToInt32(reader["CurrentStock"].ToString());
                            medicine.ExpiryDate = Convert.ToDateTime(reader["ExpiryDate"].ToString());
                            medicine.Price = Convert.ToDecimal(reader["Price"].ToString());

                            medicines.Add(medicine);
                        }
                    }
                }
            }

            return medicines;
        }
        #endregion
        #region add medcine 
        public int AddMedicine(AddMedicineViewModel model)
        {
            int newId = 0;

            string connectionString = _configuration.GetConnectionString("ConnStringMVC");

            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddMedicine", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MedicineName", model.MedicineName);
                cmd.Parameters.AddWithValue("@Description", model.Description);
                cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
                cmd.Parameters.AddWithValue("@ExpiryDate", model.ExpiryDate);
                cmd.Parameters.AddWithValue("@Price", model.Price);
                cmd.Parameters.AddWithValue("@CategoryId", model.CategoryId);

                newId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return newId;
        }
        #endregion

    }
}
