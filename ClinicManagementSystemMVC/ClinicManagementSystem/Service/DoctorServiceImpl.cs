using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;

namespace ClinicManagementSystem.Services
{
    public class DoctorServiceImpl : IDoctorService
    {
        private readonly IDoctorRepository _repo;

        public DoctorServiceImpl(IDoctorRepository repo)
        {
            _repo = repo;
        }
        public void AddDiagnosis(Diagnosis model)
        {
              _repo.AddDiagnosis(model);
        }

        public void AddLabPrescription(LabPrescription model)
        {
            _repo.AddLabPrescription(model);
        }

        public void AddMedicinePrescription(MedicinePrescription model)
        {
            _repo.AddMedicinePrescription(model);

        }

        public List<LabTest> GetLabTests()
        {
            return _repo.GetLabTests();
        }

        public List<Medicine> GetMedicines()
        {
            return _repo.GetMedicines();

        }

        public List<Appointment> GetTodaysAppointments(int doctorId)
        {
            return _repo.GetTodaysAppointments(doctorId);

        }
        public List<MedicinePrescription> GetMedicinesByAppointment(int appointmentId)
        {
            return _repo.GetMedicinesByAppointment(appointmentId);
        }

        public List<LabPrescription> GetLabTestsByAppointment(int appointmentId)
        {
            return _repo.GetLabTestsByAppointment(appointmentId);
        }
        public List<Diagnosis> GetPatientHistory(int patientId)
        {
            return _repo.GetPatientHistory(patientId);
        }
        public void MarkAppointmentCompleted(int appointmentId)
        {
            _repo.MarkAppointmentCompleted(appointmentId);
        }



    }
}
