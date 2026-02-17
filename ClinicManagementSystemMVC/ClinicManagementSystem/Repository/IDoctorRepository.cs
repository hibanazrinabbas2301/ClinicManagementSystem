using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IDoctorRepository
    {
        // ===============================
        // Dashboard
        // ===============================
        List<Appointment> GetTodaysAppointments(int doctorId);

        // ===============================
        // Diagnosis
        // ===============================
        void AddDiagnosis(Diagnosis model);

        // ✅ NEW: Load saved Diagnosis (to keep values visible)
        Diagnosis GetDiagnosisByAppointment(int appointmentId);

        // ===============================
        // Medicine Prescription
        // ===============================
        void AddMedicinePrescription(MedicinePrescription model);

        List<MedicinePrescription> GetMedicinesByAppointment(int appointmentId);

        // ===============================
        // Lab Prescription
        // ===============================
        void AddLabPrescription(LabPrescription model);

        List<LabPrescription> GetLabTestsByAppointment(int appointmentId);

        // ===============================
        // Dropdown lists
        // ===============================
        List<Medicine> GetMedicines();
        List<LabTest> GetLabTests();

        // ===============================
        // Patient History
        // ===============================
        List<Diagnosis> GetPatientHistory(int patientId);

        // ===============================
        // Appointment Completion
        // ===============================
        void MarkAppointmentCompleted(int appointmentId);
    }
}
