using ClinicManagementSystem.Models;
using ClinicManagementSystem.View_Model;
using ClinicManagementSystem.ViewModel;

namespace ClinicManagementSystem.Services
{
    public interface IDoctorService
    {
        // ===============================
        // Dashboard
        // ===============================
        List<Appointment> GetTodaysAppointments(int doctorId);

        // ===============================
        // Diagnosis
        // ===============================
        void AddDiagnosis(Diagnosis model);

        // ✅ NEW: Reload saved Diagnosis
        Diagnosis GetDiagnosisByAppointment(int appointmentId);

        // ===============================
        // Medicine Prescription
        // ===============================
        List<Medicine> GetMedicines();
        //void AddMedicinePrescription(MedicinePrescription model);
        void AddMedicinePrescription(PrescriptionDetailViewModel model);
        List<MedicinePrescription> GetMedicinesByAppointment(int appointmentId);

        // ===============================
        // Lab Prescription
        // ===============================
        List<LabTest> GetLabTests();
        void AddLabPrescription(LabPrescription model);
        List<LabPrescription> GetLabTestsByAppointment(int appointmentId);

        // ===============================
        // Patient History
        // ===============================
        List<Diagnosis> GetPatientHistory(int patientId);
        PatientBasicInfo GetPatientBasicDetails(int patientId);
        List<MedicinePrescription> GetPatientMedicineHistory(int patientId);

        List<LabPrescription> GetPatientLabHistory(int patientId);

        List<DoctorLabResultViewModel> GetLabResultsByAppointment(int appointmentId);        // ===============================
        // Appointment Completion
        // ===============================
        void MarkAppointmentCompleted(int appointmentId);
    }
}
