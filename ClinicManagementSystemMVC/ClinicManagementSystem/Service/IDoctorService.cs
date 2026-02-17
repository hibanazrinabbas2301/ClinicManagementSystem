using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface IDoctorService
    {
        List<Appointment> GetTodaysAppointments(int doctorId);

        void AddDiagnosis(Diagnosis model);

        List<Medicine> GetMedicines();

        void AddMedicinePrescription(MedicinePrescription model);

        List<LabTest> GetLabTests();

        void AddLabPrescription(LabPrescription model);

        List<MedicinePrescription> GetMedicinesByAppointment(int appointmentId);
        List<LabPrescription> GetLabTestsByAppointment(int appointmentId);
        List<Diagnosis> GetPatientHistory(int patientId);
        void MarkAppointmentCompleted(int appointmentId);



    }
}