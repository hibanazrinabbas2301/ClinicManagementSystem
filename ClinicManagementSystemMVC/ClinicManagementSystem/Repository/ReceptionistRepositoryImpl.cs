
using ClinicManagementSystem.Models;
using ClinicManagementSystem_Final.Repository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicManagementSystem.Repositories
{
    public class ReceptionistRepositoryImpl : IReceptionistRepository
    {
        private readonly string _connectionString;

        public ReceptionistRepositoryImpl(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnStringMVC");
        }
        // --------------------- Patient Management ---------------------
        public Patient GetPatientById(int id)
        {
            Patient patient = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetPatientById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PatientId", id);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        patient = new Patient
                        {
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            MMRNo = reader["MMRNo"].ToString(),
                            PatientName = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            DOB = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]) : DateTime.MinValue,
                            ContactNumber = reader["Phone"].ToString(),
                            Address = reader["Address"].ToString(),
                            BloodGroup = reader["BloodGroup"] != DBNull.Value ? reader["BloodGroup"].ToString() : string.Empty,
                            Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : string.Empty
                        };
                    }
                }
            }

            return patient;
        }

        public void AddPatient(Patient patient)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_AddPatient", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MMRNo", patient.MMRNo);
                    cmd.Parameters.AddWithValue("@PatientName", patient.PatientName);
                    cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                    cmd.Parameters.AddWithValue("@DOB", patient.DOB);
                    cmd.Parameters.AddWithValue("@ContactNumber", patient.ContactNumber);
                    cmd.Parameters.AddWithValue("@Address", patient.Address);
                    // Optional parameters
                    cmd.Parameters.AddWithValue("@BloodGroup", string.IsNullOrEmpty(patient.BloodGroup) ? (object)DBNull.Value : patient.BloodGroup);
                    cmd.Parameters.AddWithValue("@Email", (object?)patient.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", patient.Status ?? "Active");

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePatient(Patient patient)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UpdatePatient", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PatientId", patient.PatientId);
                cmd.Parameters.AddWithValue("@Name", patient.PatientName);
                cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                cmd.Parameters.AddWithValue("@DOB", patient.DOB);
                cmd.Parameters.AddWithValue("@Phone", patient.ContactNumber);
                cmd.Parameters.AddWithValue("@Address", patient.Address);
                cmd.Parameters.AddWithValue("@BloodGroup", patient.BloodGroup ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", patient.Status ?? (object)DBNull.Value);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePatient(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_DeletePatient", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PatientId", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Patient> SearchPatientsByMMRNo(string mmrNo)
        {
            List<Patient> patients = new List<Patient>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_SearchPatientByMMRNo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MMRNo", mmrNo);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            MMRNo = reader["MMRNo"].ToString(),
                            PatientName = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            DOB = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]) : DateTime.MinValue,
                            ContactNumber = reader["Phone"].ToString(),
                            Address = reader["Address"].ToString(),
                            BloodGroup = reader["BloodGroup"] != DBNull.Value ? reader["BloodGroup"].ToString() : string.Empty,
                            Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : string.Empty
                        });
                    }
                }
            }

            return patients;
        }

        public List<Patient> SearchPatientsByPhone(string phone)
        {
            List<Patient> patients = new List<Patient>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT PatientId, MMRNo, Name, Gender, DOB, Phone, Address, BloodGroup, Status
                               FROM Patient
                               WHERE Phone LIKE '%' + @Phone + '%' AND Status='Active'";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Phone", phone);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            MMRNo = reader["MMRNo"].ToString(),
                            PatientName = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            DOB = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]) : DateTime.MinValue,
                            ContactNumber = reader["Phone"].ToString(),
                            Address = reader["Address"].ToString(),
                            BloodGroup = reader["BloodGroup"] != DBNull.Value ? reader["BloodGroup"].ToString() : string.Empty,
                            Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : string.Empty
                        });
                    }
                }
            }
            return patients;
        }

        public List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT PatientId, MMRNo, Name, Gender, DOB, Phone, Address, BloodGroup, Status
                               FROM Patient
                               WHERE Status = 'Active'";

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            MMRNo = reader["MMRNo"].ToString(),
                            PatientName = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            DOB = reader["DOB"] != DBNull.Value ? Convert.ToDateTime(reader["DOB"]) : DateTime.MinValue,
                            ContactNumber = reader["Phone"].ToString(),
                            Address = reader["Address"].ToString(),
                            BloodGroup = reader["BloodGroup"] != DBNull.Value ? reader["BloodGroup"].ToString() : string.Empty,
                            Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : string.Empty
                        });
                    }
                }
            }
            return patients;
        }

        // --------------------- Doctors & Appointments ---------------------
        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT d.DoctorId, s.Name AS DoctorName, sp.Name AS Specialization, s.Phone AS ContactNumber, s.Email, s.Status
                               FROM Doctor d
                               INNER JOIN Staff s ON d.StaffId = s.StaffId
                               LEFT JOIN Specialization sp ON d.SpecializationId = sp.SpecializationId
                               WHERE s.Status = 'Active'";

                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        doctors.Add(new Doctor
                        {
                            DoctorId = Convert.ToInt32(reader["DoctorId"]),
                            DoctorName = reader["DoctorName"].ToString(),
                            Specialization = new Specialization { SpecializationName = reader["Specialization"] != DBNull.Value ? reader["Specialization"].ToString() : string.Empty },
                            ContactNumber = reader["ContactNumber"]?.ToString(),
                            Email = reader["Email"]?.ToString(),
                            IsActive = reader["Status"].ToString() == "Active"
                        });
                    }
                }
            }

            return doctors;
        }

        public List<AppointmentViewModel> GetAppointmentsByDate(DateTime slotDate)
        {
            List<AppointmentViewModel> appointments = new List<AppointmentViewModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"
            SELECT
                a.AppointmentId,
                a.PatientId,
                p.Name AS PatientName,
                a.DoctorId,
                st.Name AS DoctorName,
                sp.Name AS SpecializationName,
                a.TokenNumber,
                a.Status,
                a.ConsultationBill,
                ds.SlotId,
                ds.SlotDate,
                ds.StartTime,
                ds.EndTime
            FROM Appointment a
            INNER JOIN DoctorSlot ds ON a.SlotId = ds.SlotId
            INNER JOIN Patient p ON a.PatientId = p.PatientId
            INNER JOIN Doctor d ON a.DoctorId = d.DoctorId
            INNER JOIN Staff st ON d.StaffId = st.StaffId
            LEFT JOIN Specialization sp ON d.SpecializationId = sp.SpecializationId
            WHERE ds.SlotDate = @SlotDate
            ORDER BY ds.StartTime", con);

                cmd.Parameters.Add("@SlotDate", SqlDbType.Date).Value = slotDate.Date;

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointments.Add(new AppointmentViewModel
                        {
                            AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                            PatientId = Convert.ToInt32(reader["PatientId"]),
                            TokenNumber = reader["TokenNumber"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TokenNumber"]),
                            PatientName = reader["PatientName"].ToString(),
                            DoctorName = reader["DoctorName"].ToString(),

                            // ✅ THESE THREE WERE MISSING
                            SlotDate = Convert.ToDateTime(reader["SlotDate"]),
                            StartTime = (TimeSpan)reader["StartTime"],
                            EndTime = (TimeSpan)reader["EndTime"],

                            Status = reader["Status"].ToString()
                        });
                    }
                }
            }

            return appointments;
        }




        public int BookAppointment(int slotId, int patientId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_BookSlotAppointment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SlotId", slotId);
                cmd.Parameters.AddWithValue("@PatientId", patientId);

                con.Open();
                cmd.ExecuteNonQuery();  

                return 1; // success flag
            }
        }

        public bool GenerateConsultationBill(int appointmentId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GenerateConsultationBill", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // If SP returned a row -> success
                    return reader.Read();
                }
            }
        }

        //  Method 2: Get Consultation Bill Details
        public BillViewModel GetConsultationBillDetails(int appointmentId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GenerateConsultationBill", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    return new BillViewModel
                    {
                        AppointmentId = Convert.ToInt32(reader["AppointmentId"]),
                        PatientName = reader["PatientName"].ToString(),
                        DoctorName = reader["DoctorName"].ToString(),
                        AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                        TotalAmount = Convert.ToDecimal(reader["ConsultationBill"]),
                        BillType = "Consultation",
                        BillDate = DateTime.Now,
                        Paid = false
                    };
                }
            }
        }

        public List<SlotViewModel> GetAvailableSlots()
        {
            List<SlotViewModel> slots = new List<SlotViewModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"
            SELECT 
                ds.SlotId,
                CONCAT(
                    FORMAT(ds.StartTime, 'hh:mm tt'),
                    ' - ',
                    s.Name
                ) AS DisplayText
            FROM DoctorSlot ds
            INNER JOIN Doctor d ON ds.DoctorId = d.DoctorId
            INNER JOIN Staff s ON d.StaffId = s.StaffId
            WHERE ds.IsBooked = 0
              AND ds.SlotDate >= CAST(GETDATE() AS DATE)
            ORDER BY ds.SlotDate, ds.StartTime
        ", con);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        slots.Add(new SlotViewModel
                        {
                            SlotId = Convert.ToInt32(reader["SlotId"]),
                            DisplayText = reader["DisplayText"].ToString()
                        });
                    }
                }
            }

            return slots;
        }
        public List<SlotViewModel> GetAvailableDoctorSlots(DateTime slotDate)
        {
            var slots = new List<SlotViewModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_GetAvailableDoctorSlots", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SlotDate", slotDate.Date);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Build display text: "10:00 AM - 10:15 AM - Dr. Rajesh Kumar (Cardiologist)"
                        TimeSpan start = (TimeSpan)reader["StartTime"];
                        TimeSpan end = (TimeSpan)reader["EndTime"];

                        string display = $"{DateTime.Today.Add(start):hh:mm tt} - {DateTime.Today.Add(end):hh:mm tt} - " +
                                         $"{reader["DoctorName"]} ({reader["SpecializationName"]})";

                        slots.Add(new SlotViewModel
                        {
                            SlotId = Convert.ToInt32(reader["SlotId"]),
                            DisplayText = display
                        });
                    }
                }
            }

            return slots;
        }

        public List<AppointmentViewModel> GetTodaysAppointments()
        {
            var list = new List<AppointmentViewModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_ViewTodaysAppointments", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new AppointmentViewModel
                        {
                            AppointmentId = Convert.ToInt32(dr["AppointmentId"]),
                            PatientId = Convert.ToInt32(dr["PatientId"]),
                            PatientName = dr["PatientName"].ToString(),
                            DoctorId = Convert.ToInt32(dr["DoctorId"]),
                            DoctorName = dr["DoctorName"].ToString(),
                            SlotId = Convert.ToInt32(dr["SlotId"]),
                            SlotDate = dr["SlotDate"] == DBNull.Value
    ? DateTime.MinValue
    : Convert.ToDateTime(dr["SlotDate"]),
                            StartTime = TimeSpan.Parse(dr["StartTime"].ToString()),
                            EndTime = TimeSpan.Parse(dr["EndTime"].ToString()),
                            TokenNumber = dr["TokenNumber"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TokenNumber"]),
                            Status = dr["Status"].ToString(),
                            ConsultationBill = dr["ConsultationBill"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ConsultationBill"]),
                            //AppointmentDate = Convert.ToDateTime(dr["SlotDate"])
                        });
                    }
                }
            }

            return list;
        }

        public List<AppointmentViewModel> GetUpcomingAppointments()
        {
            var list = new List<AppointmentViewModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_ViewUpcomingAppointments", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new AppointmentViewModel
                        {
                            AppointmentId = Convert.ToInt32(dr["AppointmentId"]),
                            PatientId = Convert.ToInt32(dr["PatientId"]),
                            PatientName = dr["PatientName"].ToString(),
                            DoctorId = Convert.ToInt32(dr["DoctorId"]),
                            DoctorName = dr["DoctorName"].ToString(),
                            SlotId = Convert.ToInt32(dr["SlotId"]),
                            SlotDate = Convert.ToDateTime(dr["SlotDate"]),
                            StartTime = TimeSpan.Parse(dr["StartTime"].ToString()),
                            EndTime = TimeSpan.Parse(dr["EndTime"].ToString()),
                            TokenNumber = dr["TokenNumber"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TokenNumber"]),
                            Status = dr["Status"].ToString(),
                            ConsultationBill = dr["ConsultationBill"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ConsultationBill"]),
                            AppointmentDate = Convert.ToDateTime(dr["SlotDate"])
                        });
                    }
                }
            }

            return list;
        }

        public List<AppointmentViewModel> SearchAppointments(string searchText, string type)
        {
            var list = new List<AppointmentViewModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_SearchAppointments", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchText", string.IsNullOrWhiteSpace(searchText) ? (object)DBNull.Value : searchText);
                cmd.Parameters.AddWithValue("@Type", string.IsNullOrWhiteSpace(type) ? "ALL" : type);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new AppointmentViewModel
                        {
                            AppointmentId = Convert.ToInt32(dr["AppointmentId"]),
                            PatientId = Convert.ToInt32(dr["PatientId"]),
                            PatientName = dr["PatientName"].ToString(),
                            DoctorId = Convert.ToInt32(dr["DoctorId"]),
                            DoctorName = dr["DoctorName"].ToString(),
                            SlotId = Convert.ToInt32(dr["SlotId"]),
                            SlotDate = Convert.ToDateTime(dr["SlotDate"]),
                            StartTime = (TimeSpan)dr["StartTime"],
                            EndTime = (TimeSpan)dr["EndTime"],
                            TokenNumber = dr["TokenNumber"] == DBNull.Value ? null : (int?)Convert.ToInt32(dr["TokenNumber"]),
                            Status = dr["Status"].ToString(),
                            ConsultationBill = dr["ConsultationBill"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["ConsultationBill"]),
                            AppointmentDate = Convert.ToDateTime(dr["SlotDate"]),
                            MMRNo = dr["MMRNo"].ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}
