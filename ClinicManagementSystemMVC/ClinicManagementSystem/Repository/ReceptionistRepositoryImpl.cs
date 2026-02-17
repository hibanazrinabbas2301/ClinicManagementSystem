using ClinicManagementSystem_Final.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicManagementSystem_Final.Repository
{
    public class ReceptionistRepositoryImpl : IReceptionistRepository
    {
        private readonly string _connectionString;

        public ReceptionistRepositoryImpl(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnStrMVC");
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
                            AppointmentId = reader["AppointmentId"] != DBNull.Value ? Convert.ToInt32(reader["AppointmentId"]) : 0,
                            PatientId = reader["PatientId"] != DBNull.Value ? Convert.ToInt32(reader["PatientId"]) : 0,
                            TokenNumber = reader["TokenNumber"] != DBNull.Value ? Convert.ToInt32(reader["TokenNumber"]) : 0,
                            PatientName = reader["PatientName"].ToString(),
                            DoctorName = reader["DoctorName"].ToString(),
                            AppointmentDate = Convert.ToDateTime(reader["SlotDate"]),
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

        public bool GenerateConsultationBill(int patientId, int appointmentId, decimal consultationFee)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();

                    // 🔹 Step 1: Check if a bill already exists for this appointment
                    string checkQuery = "SELECT COUNT(*) FROM Bill WHERE AppointmentId = @AppointmentId AND BillType = 'Consultation'";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            Console.WriteLine("Bill already exists for this appointment.");
                            return false;
                        }
                    }

                    // 🔹 Step 2: Insert into Bill table
                    string billQuery = @"
                        INSERT INTO Bill (PatientId, AppointmentId, BillType, TotalAmount, BillDate, Paid)
                        VALUES (@PatientId, @AppointmentId, 'Consultation', @TotalAmount, GETDATE(), 0);
                        SELECT SCOPE_IDENTITY();";

                    int billId;
                    using (SqlCommand cmd = new SqlCommand(billQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientId", patientId);
                        cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                        cmd.Parameters.AddWithValue("@TotalAmount", consultationFee);

                        billId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 🔹 Step 3: Insert into BillDetails table
                    string detailQuery = @"
                        INSERT INTO BillDetails (BillId, ItemType, Quantity, UnitPrice, Amount)
                        VALUES (@BillId, 'Consultation Fee', 1, @ConsultationFee, @ConsultationFee)";

                    using (SqlCommand cmd2 = new SqlCommand(detailQuery, con))
                    {
                        cmd2.Parameters.AddWithValue("@BillId", billId);
                        cmd2.Parameters.AddWithValue("@ConsultationFee", consultationFee);
                        cmd2.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error generating consultation bill: " + ex.Message);
                return false;
            }
        }

        //  Method 2: Get Consultation Bill Details
        public BillViewModel GetConsultationBillDetails(int patientId, int appointmentId)
        {
            BillViewModel bill = null;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = @"
                        SELECT TOP 1 
                            b.BillId,
                            p.Name AS PatientName,
                            s.Name AS DoctorName,
                            a.AppointmentDate,
                            b.TotalAmount,
                            b.Paid
                        FROM Bill b
                        INNER JOIN Appointment a ON b.AppointmentId = a.AppointmentId
                        INNER JOIN Patient p ON b.PatientId = p.PatientId
                        INNER JOIN Doctor d ON a.DoctorId = d.DoctorId
                        INNER JOIN Staff s ON d.StaffId = s.StaffId
                        WHERE b.PatientId = @PatientId AND b.AppointmentId = @AppointmentId
                        ORDER BY b.BillDate DESC;";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientId", patientId);
                        cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bill = new BillViewModel
                                {
                                    BillId = Convert.ToInt32(reader["BillId"]),
                                    PatientName = reader["PatientName"].ToString(),
                                    DoctorName = reader["DoctorName"].ToString(),
                                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]),
                                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                    Paid = Convert.ToBoolean(reader["Paid"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching consultation bill details: " + ex.Message);
            }

            return bill;
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
    }
}
