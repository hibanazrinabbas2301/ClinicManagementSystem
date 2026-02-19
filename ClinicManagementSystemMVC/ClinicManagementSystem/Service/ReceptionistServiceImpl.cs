
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Service;
using ClinicManagementSystem_Final.Repository;

namespace ClinicManagementSystem.Services
{
    public class ReceptionistServiceImpl : IReceptionistService
        {
            private readonly IReceptionistRepository _receptionistRepository;
            private readonly IEmailService _emailService;

        public ReceptionistServiceImpl(
                                        IReceptionistRepository receptionistRepository,
                                        IEmailService emailService)
        {
            _receptionistRepository = receptionistRepository;
            _emailService = emailService;
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

            public List<AppointmentViewModel> GetAppointmentsByDate(DateTime slotDate)
            {
                return _receptionistRepository.GetAppointmentsByDate(slotDate);
            }
            public int BookAppointment(int slotId, int patientId)
            {
                return _receptionistRepository.BookAppointment(slotId, patientId);
            }

            public bool GenerateConsultationBill(int appointmentId)
            {
                return _receptionistRepository.GenerateConsultationBill(appointmentId);
            }

                 
            public BillViewModel GetConsultationBillDetails(int appointmentId)

            {
                return _receptionistRepository.GetConsultationBillDetails(appointmentId);
            }


      

            public List<SlotViewModel> GetAvailableSlots()
            {
                return _receptionistRepository.GetAvailableSlots();
            }


            public List<SlotViewModel> GetAvailableDoctorSlots(DateTime slotDate)
            {
                return _receptionistRepository.GetAvailableDoctorSlots(slotDate);
            }
            public void SendConsultationBillEmail(int appointmentId, string email)
            {
                var bill = _receptionistRepository.GetConsultationBillDetails(appointmentId);

                if (bill == null)
                    throw new Exception("Bill not found.");

                _emailService.SendBill(email, bill);
            }
    
    }
}
