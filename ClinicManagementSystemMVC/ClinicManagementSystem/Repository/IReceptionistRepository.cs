using ClinicManagementSystem.Models;

namespace ClinicManagementSystem_Final.Repository
{
    public interface IReceptionistRepository
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
        //List<AppointmentViewModel> GetAppointments();
        List<AppointmentViewModel> GetAppointmentsByDate(DateTime slotDate);

        int BookAppointment(int slotId, int patientId);
        bool GenerateConsultationBill(int appointmentId);
        BillViewModel GetConsultationBillDetails(int appointmentId);

        List<SlotViewModel> GetAvailableSlots();
        List<SlotViewModel> GetAvailableDoctorSlots(DateTime slotDate);
    }
}
