using ClassLibraryDataBaseConnection;
using ClinicManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicManagementSystem.Repositories
{
    public class LabTechnicianRepository : ILabTechnicianRepository
    {
        private readonly string connectionString;

        public LabTechnicianRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ConnStringMVC");
        }

        #region Get All Lab Tests
        public IEnumerable<LabTest> GetAllLabTests()
        {
            List<LabTest> list = new List<LabTest>();

            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetLabTestDropdown", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LabTest
                        {
                            TestId = Convert.ToInt32(reader["TestId"]),
                            TestName = reader["TestName"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            SampleType = reader["SampleType"].ToString(),
                            NormalRange = reader["NormalRange"].ToString()
                        });
                    }
                }
            }

            return list;
        }
        #endregion


        #region Add Lab Test
        public void AddLabTest(LabTest labTest)
        {
            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddLabTest", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TestName", labTest.TestName);
                cmd.Parameters.AddWithValue("@Price", labTest.Price);
                cmd.Parameters.AddWithValue("@SampleType", labTest.SampleType);
                cmd.Parameters.AddWithValue("@NormalRange", labTest.NormalRange);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion


        #region Get Lab Test By Id
        public LabTest GetLabTestById(int testId)
        {
            LabTest labTest = null;

            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM LabTest WHERE TestId=@TestId", connection);
                cmd.Parameters.AddWithValue("@TestId", testId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        labTest = new LabTest
                        {
                            TestId = Convert.ToInt32(reader["TestId"]),
                            TestName = reader["TestName"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            SampleType = reader["SampleType"].ToString(),
                            NormalRange = reader["NormalRange"].ToString()
                        };
                    }
                }
            }

            return labTest;
        }
        #endregion


        #region Edit Lab Test
        public void EditLabTest(LabTest labTest)
        {
            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_EditLabTest", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TestId", labTest.TestId);
                cmd.Parameters.AddWithValue("@TestName", labTest.TestName);
                cmd.Parameters.AddWithValue("@Price", labTest.Price);
                cmd.Parameters.AddWithValue("@SampleType", labTest.SampleType);
                cmd.Parameters.AddWithValue("@NormalRange", labTest.NormalRange);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion


        #region View Pending Tests
        public IEnumerable<LabTestPrescription> GetPendingTests()
        {
            List<LabTestPrescription> list = new List<LabTestPrescription>();

            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_LabTech_PendingTests", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LabTestPrescription
                        {
                            PrescriptionId = Convert.ToInt32(reader["PrescriptionId"]),
                            AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            PatientName = reader["PatientName"].ToString(),
                            DoctorId = Convert.ToInt32(reader["DoctorId"]),
                            DoctorName = reader["DoctorName"].ToString(),
                            TestId = Convert.ToInt32(reader["TestId"]),
                            TestName = reader["TestName"].ToString(),
                            SampleType = reader["SampleType"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"]),
                            NormalRange = reader["NormalRange"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"]),
                            Status = reader["Status"].ToString(),
                            PrescribedDate = Convert.ToDateTime(reader["PrescribedDate"])
                        });
                    }
                }
            }

            return list;
        }
        #endregion


        #region Add Lab Result
        public int AddLabResult(LabResult result)
        {
            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_AddLabResult", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TestId", result.TestId);
                cmd.Parameters.AddWithValue("@PatientId", result.PatientId);
                cmd.Parameters.AddWithValue("@NormalRange", result.NormalRange);
                cmd.Parameters.AddWithValue("@ActualValue", result.ActualValue);
                cmd.Parameters.AddWithValue("@Remarks", result.Remarks);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        #endregion


        #region Update Test Status
        public void UpdateLabTestStatus(int prescriptionId, string status)
        {
            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateLabTestStatus", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PrescriptionId", prescriptionId);
                cmd.Parameters.AddWithValue("@Status", status);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion


        #region Generate Lab Bill
        public void GenerateLabBill(int resultId, decimal amount)
        {
            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GenerateLabBill", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ResultId", resultId);
                cmd.Parameters.AddWithValue("@LabBillAmount", amount);

                cmd.ExecuteNonQuery();
            }
        }
        #endregion


        #region Get Result By Id (For PDF + Email)
        public LabResult GetResultById(int resultId)
        {
            LabResult result = null;

            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM LabResult WHERE ResultId=@ResultId", connection);
                cmd.Parameters.AddWithValue("@ResultId", resultId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new LabResult
                        {
                            ResultId = Convert.ToInt32(reader["ResultId"]),
                            TestId = Convert.ToInt32(reader["TestId"]),
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            NormalRange = reader["NormalRange"].ToString(),
                            ActualValue = Convert.ToDecimal(reader["ActualValue"]),
                            Remarks = reader["Remarks"].ToString(),
                            DoctorReview = reader["DoctorReview"].ToString(),
                            Date = Convert.ToDateTime(reader["Date"])
                        };
                    }
                }
            }

            return result;
        }
        #endregion


        #region View Patient Lab Reports
        public IEnumerable<LabResult> GetPatientLabReports(int patientId)
        {
            List<LabResult> list = new List<LabResult>();

            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ViewPatientLabReports", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PatientId", patientId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new LabResult
                        {
                            ResultId = Convert.ToInt32(reader["ResultId"]),
                            ActualValue = Convert.ToDecimal(reader["ActualValue"]),
                            Remarks = reader["Remarks"].ToString(),
                            DoctorReview = reader["DoctorReview"].ToString(),
                            Date = Convert.ToDateTime(reader["Date"])
                        });
                    }
                }
            }

            return list;
        }
        #endregion


        #region View Completed Tests
        public IEnumerable<LabTestPrescription> GetCompletedTests()
        {
            List<LabTestPrescription> list = new List<LabTestPrescription>();

            using (SqlConnection connection = ConnectionManager.OpenConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_LabTech_CompletedTests", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new LabTestPrescription
                    {
                        PrescriptionId = Convert.ToInt32(reader["PrescriptionId"]),
                        AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                        PatientId = Convert.ToInt32(reader["PatientId"]),
                        DoctorId = Convert.ToInt32(reader["DoctorId"]),

                        ResultId = reader["ResultId"] != DBNull.Value
                                    ? Convert.ToInt32(reader["ResultId"])
                                    : (int?)null,

                        ActualValue = reader["ActualValue"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["ActualValue"])
                                    : (decimal?)null,

                        Remarks = reader["Remarks"].ToString(),

                        ResultDate = reader["ResultDate"] != DBNull.Value
                 ? Convert.ToDateTime(reader["ResultDate"])
                 : (DateTime?)null,

                        PatientName = reader["PatientName"].ToString(),
                        DoctorName = reader["DoctorName"].ToString(),
                        TestId = Convert.ToInt32(reader["TestId"]),
                        TestName = reader["TestName"].ToString(),
                        SampleType = reader["SampleType"].ToString(),
                        NormalRange = reader["NormalRange"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Status = reader["Status"].ToString(),
                        PrescribedDate = Convert.ToDateTime(reader["PrescribedDate"])
                    });
                }

                reader.Close();
            }

            return list;
        }

        #endregion

    }
}
