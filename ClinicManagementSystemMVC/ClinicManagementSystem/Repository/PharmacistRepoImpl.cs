using ClassLibraryDataBaseConnection;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.ViewModel;
using Microsoft.Data.SqlClient;
using System.Data;


namespace ClinicManagementSystem.Repositories
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
                    cmd.CommandType = CommandType.StoredProcedure;

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

        #region categories 
        public IEnumerable<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            string connectionString = _configuration.GetConnectionString("ConnStringMVC");

            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT CategoryId, CategoryName FROM Category",
                    connection);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            CategoryName = reader["CategoryName"].ToString() ?? ""
                        });
                    }
                }
            }

            return categories;
        }
        #endregion

        #region edit medcine stock
        
        public int UpdateMedicine(UpdateMedicineViewModel model)
        {
            int rowsAffected = 0;

            string connectionString = _configuration.GetConnectionString("ConnStringMVC");

            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                if (connection != null)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateMedicineStock", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@MedicineId", model.MedicineId);
                        cmd.Parameters.AddWithValue("@MedicineName", model.MedicineName);
                        cmd.Parameters.AddWithValue("@Description", model.MedicineDescription);
                        cmd.Parameters.AddWithValue("@CategoryId", model.CategoryId);
                        cmd.Parameters.AddWithValue("@AddedQuantity", model.AddedQuantity);

                        if (model.NewPrice.HasValue)
                            cmd.Parameters.AddWithValue("@NewPrice", model.NewPrice);
                        else
                            cmd.Parameters.AddWithValue("@NewPrice", DBNull.Value);

                        if (model.NewExpiry.HasValue)
                            cmd.Parameters.AddWithValue("@NewExpiry", model.NewExpiry);
                        else
                            cmd.Parameters.AddWithValue("@NewExpiry", DBNull.Value);

                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            }

            return rowsAffected;
        }
        #endregion

        public IEnumerable<PrescriptionViewModel> GetPendingPrescriptions()
        {
            List<PrescriptionViewModel> list = new List<PrescriptionViewModel>();

            string connectionString = _configuration.GetConnectionString("ConnStringMVC");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();   // 🚨 THIS IS IMPORTANT

                using (SqlCommand cmd = new SqlCommand("sp_Pharmacist_PendingPrescriptions", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PrescriptionViewModel
                            {
                                PrescriptionId = Convert.ToInt32(reader["PrescriptionId"]),
                                PatientName = reader["PatientName"].ToString(),
                                MedicineName = reader["MedicineName"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Frequency = reader["Frequency"].ToString(),
                                DurationDays = Convert.ToInt32(reader["DurationDays"]),
                                Status = reader["Status"].ToString(),
                                PrescribedDate = Convert.ToDateTime(reader["PrescribedDate"])
                            });
                        }
                    }
                }
            }

            return list;
        }

        public IEnumerable<PrescriptionViewModel> GetPrescriptionDetailsById(int prescriptionId)
        {
            List<PrescriptionViewModel> list = new List<PrescriptionViewModel>();

            string connectionString = _configuration.GetConnectionString("ConnStringMVC");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Pharmacist_PrescriptionDetails", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PrescriptionId", prescriptionId);

                connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PrescriptionViewModel
                        {
                            PrescriptionId = Convert.ToInt32(reader["PrescriptionId"]),
                            PatientName = reader["PatientName"].ToString(),
                            MedicineName = reader["MedicineName"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            Frequency = reader["Frequency"].ToString(),
                            DurationDays = Convert.ToInt32(reader["DurationDays"]),
                            Status = reader["Status"].ToString()
                        });
                    }
                }
            }

            return list;
        }



        public int IssuePrescription(int prescriptionId)
        {
            int result = 0;

            string connectionString = _configuration.GetConnectionString("ConnStringMVC");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_IssuePrescription", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PrescriptionId", prescriptionId);

                    connection.Open();

                    object response = cmd.ExecuteScalar();

                    if (response != null)
                        result = Convert.ToInt32(response);
                }
            }

            return result;
        }



    }
}
