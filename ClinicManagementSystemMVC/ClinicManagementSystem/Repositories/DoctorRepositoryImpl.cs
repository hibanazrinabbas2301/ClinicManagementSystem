using ClinicManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicManagementSystem.Repositories
{
    public class DoctorRepositoryImpl : IDoctorRepository
    {
        private readonly string _ConnectionString;

        public DoctorRepositoryImpl(IConfiguration configuration)
        {
            _ConnectionString = configuration.GetConnectionString("ConnStringMVC");
        }

        // =====================================================
        // ✅ Diagnosis Insert
        // =====================================================
        public void AddDiagnosis(Diagnosis model)
        {
            using SqlConnection con = new SqlConnection(_ConnectionString);

            SqlCommand cmd = new SqlCommand("sp_AddDiagnosisDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AppointmentId", model.AppointmentId);
            cmd.Parameters.AddWithValue("@PatientId", model.PatientId);
            cmd.Parameters.AddWithValue("@DoctorId", model.DoctorId);
            cmd.Parameters.AddWithValue("@Symptoms", model.Symptoms ?? "");
            cmd.Parameters.AddWithValue("@Diagnosis", model.DiagnosisText ?? "");
            cmd.Parameters.AddWithValue("@DoctorNotes", model.DoctorNotes ?? "");

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // =====================================================
        // ✅ Add Medicine Prescription
        // =====================================================
        public void AddMedicinePrescription(MedicinePrescription model)
        {
            using SqlConnection con = new SqlConnection(_ConnectionString);

            SqlCommand cmd = new SqlCommand("sp_AddMedicinePrescription", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AppointmentId", model.AppointmentId);
            cmd.Parameters.AddWithValue("@PatientId", model.PatientId);
            cmd.Parameters.AddWithValue("@DoctorId", model.DoctorId);
            cmd.Parameters.AddWithValue("@MedicineId", model.MedicineId);
            cmd.Parameters.AddWithValue("@Quantity", model.Quantity);
            cmd.Parameters.AddWithValue("@Frequency", model.Frequency ?? "");
            cmd.Parameters.AddWithValue("@DurationDays", model.DurationDays);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // =====================================================
        // ✅ Get Prescribed Medicines for Appointment
        // =====================================================
        public List<MedicinePrescription> GetMedicinesByAppointment(int appointmentId)
        {
            List<MedicinePrescription> list = new();

            using SqlConnection con = new SqlConnection(_ConnectionString);

            SqlCommand cmd = new SqlCommand("sp_ViewMedicinesByAppointment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new MedicinePrescription()
                {
                    MedicineName = dr["MedicineName"].ToString(),
                    Quantity = Convert.ToInt32(dr["Quantity"]),
                    Frequency = dr["Frequency"].ToString(),
                    DurationDays = Convert.ToInt32(dr["DurationDays"])
                });
            }

            return list;
        }

        // =====================================================
        // ✅ Add Lab Prescription
        // =====================================================
        public void AddLabPrescription(LabPrescription model)
        {
            using SqlConnection con = new SqlConnection(_ConnectionString);

            SqlCommand cmd = new SqlCommand("sp_AddLabPrescription", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AppointmentId", model.AppointmentId);
            cmd.Parameters.AddWithValue("@PatientId", model.PatientId);
            cmd.Parameters.AddWithValue("@DoctorId", model.DoctorId);
            cmd.Parameters.AddWithValue("@TestId", model.TestId);
            cmd.Parameters.AddWithValue("@Quantity", model.Quantity);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // =====================================================
        // ✅ Get Prescribed Lab Tests for Appointment
        // =====================================================
        public List<LabPrescription> GetLabTestsByAppointment(int appointmentId)
        {
            List<LabPrescription> list = new();

            using SqlConnection con = new SqlConnection(_ConnectionString);

            SqlCommand cmd = new SqlCommand("sp_ViewLabTestsByAppointment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new LabPrescription()
                {
                    TestName = dr["TestName"].ToString(),
                    Quantity = Convert.ToInt32(dr["Quantity"]),
                    Status = dr["Status"].ToString()
                });
            }

            return list;
        }

        // =====================================================
        // ✅ Dropdown Medicines
        // =====================================================
        public List<Medicine> GetMedicines()
        {
            List<Medicine> list = new();

            using SqlConnection con = new SqlConnection(_ConnectionString);

            SqlCommand cmd = new SqlCommand("SELECT MedicineId, MedicineName FROM Medicine", con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new Medicine()
                {
                    MedicineId = Convert.ToInt32(dr["MedicineId"]),
                    MedicineName = dr["MedicineName"].ToString()
                });
            }

            return list;
        }

        // =====================================================
        // ✅ Dropdown Lab Tests
        // =====================================================
        public List<LabTest> GetLabTests()
        {
            List<LabTest> list = new();

            using SqlConnection con = new SqlConnection(_ConnectionString);

            SqlCommand cmd = new SqlCommand("SELECT TestId, TestName FROM LabTest", con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new LabTest()
                {
                    TestId = Convert.ToInt32(dr["TestId"]),
                    TestName = dr["TestName"].ToString()
                });
            }

            return list;
        }

        // =====================================================
        // ✅ Dashboard Appointments
        // =====================================================
        public List<Appointment> GetTodaysAppointments(int doctorId)
        {
            List<Appointment> list = new();

            using SqlConnection con = new SqlConnection(_ConnectionString);

            SqlCommand cmd = new SqlCommand("sp_GetTodaysAppointmentsForDoctor", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DoctorId", doctorId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new Appointment()
                {
                    AppointmentId = Convert.ToInt32(dr["AppointmentId"]),
                    PatientId = Convert.ToInt32(dr["PatientId"]),
                    PatientName = dr["PatientName"].ToString(),
                    StartTime = dr["StartTime"].ToString(),
                    EndTime = dr["EndTime"].ToString(),
                    Status = dr["AppointmentState"].ToString()
                });
            }

            return list;
        }
        public List<Diagnosis> GetPatientHistory(int patientId)
        {
            List<Diagnosis> list = new();

            using SqlConnection con = new SqlConnection(_ConnectionString);

            SqlCommand cmd = new SqlCommand("sp_PatientHistory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PatientId", patientId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new Diagnosis()
                {
                    Date = Convert.ToDateTime(dr["Date"]),
                    Symptoms = dr["Symptoms"].ToString(),
                    DiagnosisName = dr["DiagnosisName"].ToString(),
                    DoctorNotes = dr["DoctorNotes"].ToString()
                });
            }

            return list;
        }
        public void MarkAppointmentCompleted(int appointmentId)
        {
            using SqlConnection con = new SqlConnection(_ConnectionString);

            SqlCommand cmd = new SqlCommand("sp_MarkAppointmentCompleted", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);

            con.Open();
            cmd.ExecuteNonQuery();
        }

    }
}

    


