
using ClinicManagementSystem.Models;
using ClinicManagementSystem_Final.Models;
using ClinicManagementSystem_Final.Repository;

namespace ClinicManagementSystem.Services
{
    public class ReceptionistServiceImpl : IReceptionistService
        {
            private readonly IReceptionistRepository _receptionistRepository;

            public ReceptionistServiceImpl(IReceptionistRepository receptionistRepository)
            {
                _receptionistRepository = receptionistRepository;
            }

            // ---------------------- Patient Management ----------------------
            public void AddPatient(Patient patient)
            {
                _receptionistRepository.AddPatient(patient);
            }

            public Patient GetPatientById(int id)
            {
                return _receptionistRepository.GetPatientById(id);
            }

            public void UpdatePatient(Patient patient)
            {
                _receptionistRepository.UpdatePatient(patient);
            }

            public void DeletePatient(int id)
            {
                _receptionistRepository.DeletePatient(id);
            }

            public List<Patient> SearchPatientsByMMRNo(string mmrNo)
            {
                return _receptionistRepository.SearchPatientsByMMRNo(mmrNo);
            }

            public List<Patient> SearchPatientsByPhone(string phone)
            {
                return _receptionistRepository.SearchPatientsByPhone(phone);
            }

            // ---------------------- Scheduling / Appointments ----------------------
            public List<Doctor> GetAllDoctors()
            {
                return _receptionistRepository.GetAllDoctors();
            }

            public List<Patient> GetAllPatients()
            {
                return _receptionistRepository.GetAllPatients();
            }

            public List<ClinicManagementSystem_Final.Models.AppointmentViewModel> GetAppointments()
            {
                return _receptionistRepository.GetAppointments();
            }

            public int BookAppointment(int slotId, int patientId)
            {
                return _receptionistRepository.BookAppointment(slotId, patientId);
            }

            public bool GenerateConsultationBill(int patientId, int appointmentId, decimal consultationFee)
            {
                return _receptionistRepository.GenerateConsultationBill(patientId, appointmentId, consultationFee);
            }

            public ClinicManagementSystem_Final.Models.BillViewModel GetConsultationBillDetails(int patientId, int appointmentId)
            {
                return _receptionistRepository.GetConsultationBillDetails(patientId, appointmentId);
            }

        public List<ClinicManagementSystem_Final.Models.SlotViewModel> GetAvailableSlots()
        {
            return _receptionistRepository.GetAvailableSlots();
        }
    }
    }
