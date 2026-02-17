using _2026_EMS_Project_new_Batch.Models;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IReceptionistService
    {
        // Patient Management
        List<Patient> SearchPatientsByPhone(string phone);
        List<Patient> SearchPatientsByMMRNo(string mmrNo);

        //CRUD operations for Patient
        void AddPatient(Patient patient);
        Patient GetPatientById(int id);
        void UpdatePatient(Patient patient);
        void DeletePatient(int id);

        // Scheduling / Appointments
        List<Doctor> GetAllDoctors();
        List<Patient> GetAllPatients();
        List<AppointmentViewModel> GetAppointments();
        int BookAppointment(int slotId, int patientId);


        bool GenerateConsultationBill(int patientId, int appointmentId, decimal fee);
        BillViewModel GetConsultationBillDetails(int patientId, int appointmentId);
        List<SlotViewModel> GetAvailableSlots();
    }
}
