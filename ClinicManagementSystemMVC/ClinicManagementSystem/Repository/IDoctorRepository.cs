using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface IDoctorRepository
    {
        // Dashboard
        List<Appointment> GetTodaysAppointments(int doctorId);

        // Diagnosis
        void AddDiagnosis(Diagnosis model);

        // Medicine Prescription
        void AddMedicinePrescription(MedicinePrescription model);
        List<MedicinePrescription> GetMedicinesByAppointment(int appointmentId);

        // Lab Prescription
        void AddLabPrescription(LabPrescription model);
        List<LabPrescription> GetLabTestsByAppointment(int appointmentId);

        // Dropdown lists
        List<Medicine> GetMedicines();
        List<LabTest> GetLabTests();
        List<Diagnosis> GetPatientHistory(int patientId);
        void MarkAppointmentCompleted(int appointmentId);


    }
}
