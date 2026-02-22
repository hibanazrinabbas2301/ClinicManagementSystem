using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repositories;
using ClinicManagementSystem.View_Model;
using ClinicManagementSystem.ViewModel;

namespace ClinicManagementSystem.Services
{
    public class DoctorServiceImpl : IDoctorService
    {
        private readonly IDoctorRepository _repo;

        public DoctorServiceImpl(IDoctorRepository repo)
        {
            _repo = repo;
        }

        // ===============================
        // Diagnosis
        // ===============================
        public void AddDiagnosis(Diagnosis model)
        {
            _repo.AddDiagnosis(model);
        }

        // ✅ NEW: Load Diagnosis for Consultation Page
        public Diagnosis GetDiagnosisByAppointment(int appointmentId)
        {
            return _repo.GetDiagnosisByAppointment(appointmentId);
        }

        // ===============================
        // Medicine Prescription
        // ===============================
        public List<Medicine> GetMedicines()
        {
            return _repo.GetMedicines();
        }

        public void AddMedicinePrescription(PrescriptionDetailViewModel model)
        {
            // 1️⃣ Calculate Quantity
            int requiredQuantity = model.Frequency * model.DurationDays;

            // 2️⃣ Get Available Stock
            int availableStock = _repo.GetMedicineStock(model.MedicineId);

            // 3️⃣ Check Stock
            if (availableStock < requiredQuantity)
            {
                throw new Exception("Not enough stock available for this medicine.");
            }

            // 4️⃣ Convert ViewModel → Entity Model
            MedicinePrescription prescription = new MedicinePrescription
            {
                AppointmentId = model.AppointmentId,   // 🔥 THIS WAS MISSING
                PatientId = model.PatientId,           // 🔥 THIS WAS MISSING
                DoctorId = model.DoctorId,             // 🔥 THIS WAS MISSING
                MedicineId = model.MedicineId,
                Quantity = requiredQuantity,
                Dosage = model.Dosage,
                Frequency = model.Frequency,
                DurationDays = model.DurationDays
            };

            // 5️⃣ Save Prescription
            _repo.AddMedicinePrescription(prescription);
        }

        public List<MedicinePrescription> GetMedicinesByAppointment(int appointmentId)
        {
            return _repo.GetMedicinesByAppointment(appointmentId);
        }

        // ===============================
        // Lab Prescription
        // ===============================
        public List<LabTest> GetLabTests()
        {
            return _repo.GetLabTests();
        }

        public void AddLabPrescription(LabPrescription model)
        {
            _repo.AddLabPrescription(model);
        }

        public List<LabPrescription> GetLabTestsByAppointment(int appointmentId)
        {
            return _repo.GetLabTestsByAppointment(appointmentId);
        }

        // ===============================
        // Patient History
        // ===============================
        public List<Diagnosis> GetPatientHistory(int patientId)
        {
            return _repo.GetPatientHistory(patientId);
        }
        public PatientBasicInfo GetPatientBasicDetails(int patientId)
        {
            return _repo.GetPatientBasicDetails(patientId);
        }
        // ===============================
        // Appointment Completion
        // ===============================
        public void MarkAppointmentCompleted(int appointmentId)
        {
            _repo.MarkAppointmentCompleted(appointmentId);
        }
        public List<MedicinePrescription> GetPatientMedicineHistory(int patientId)
        {
            return _repo.GetPatientMedicineHistory(patientId);
        }

        public List<LabPrescription> GetPatientLabHistory(int patientId)
        {
            return _repo.GetPatientLabHistory(patientId);
        }

        public List<DoctorLabResultViewModel> GetLabResultsByAppointment(int appointmentId)
        {
            return _repo.GetLabResultsByAppointment(appointmentId);
        }
        // ===============================
        // Dashboard
        // ===============================
        public List<Appointment> GetTodaysAppointments(int doctorId)
        {
            return _repo.GetTodaysAppointments(doctorId);
        }
    }
}
