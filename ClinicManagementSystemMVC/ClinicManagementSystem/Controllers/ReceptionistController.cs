using _2026_EMS_Project_new_Batch.Models;
using _2026_EMS_Project_new_Batch.Models;
using _2026_EMS_Project_new_Batch.Repository;
using _2026_EMS_Project_new_Batch.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem_Final.Controllers
{
    public class ReceptionistController : Controller
    {
        private readonly IReceptionistService _receptionistService;

        public ReceptionistController(IReceptionistService receptionistService)
        {
            _receptionistService = receptionistService;
        }

        // ---------------- Dashboard ----------------
        public IActionResult Index()
        {
            return View(); // Loads Views/Receptionist/Index.cshtml
        }

        // ---------------- Patient Management ----------------

        // Search by MMR No or Phone No (combined)
        public IActionResult Patients(string mmrNo, string phone)
        {
            List<Patient> patients;
            
            if (!string.IsNullOrEmpty(mmrNo))
                patients = _receptionistService.SearchPatientsByMMRNo(mmrNo.Trim());
            else if (!string.IsNullOrEmpty(phone))
                patients = _receptionistService.SearchPatientsByPhone(phone.Trim());
            else
                patients = _receptionistService.GetAllPatients();

            return View(patients);
        }

        // Patient details
        public IActionResult PatientDetails(int id)
        {
            var patient = _receptionistService.GetPatientById(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        // Add new patient (GET)
        public IActionResult AddPatient()
        {
            return View();
        }

        // Add new patient (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _receptionistService.AddPatient(patient);
                TempData["SuccessMessage"] = "Patient added successfully!";
                return RedirectToAction(nameof(Patients));
            }
            TempData["ErrorMessage"] = "Failed to add patient.";
            return RedirectToAction(nameof(Patients));
        }

        // Update patient (GET)
        public IActionResult EditPatient(int id)
        {
            var patient = _receptionistService.GetPatientById(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        // Update patient (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _receptionistService.UpdatePatient(patient);
                TempData["SuccessMessage"] = "Patient updated successfully!";
                return RedirectToAction(nameof(Patients));
            }
            TempData["ErrorMessage"] = "Failed to update patient.";
            return RedirectToAction(nameof(Patients));
        }

        // ---------------- Appointments ----------------

        // GET: View today's appointments
        [HttpGet]
        public IActionResult TodayAppointments()
        {
            ViewBag.Patients = _receptionistService.GetAllPatients()
                                  ?? new List<Patient>();

            ViewBag.Slots = _receptionistService.GetAvailableSlots()
                               ?? new List<SlotViewModel>();

            var appointments = _receptionistService.GetAppointments()
                                ?? new List<AppointmentViewModel>();

            return View(appointments);
        }

        // POST: Book new appointment from modal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookAppointment(int slotId, int patientId)
        {
            try
            {
                if (slotId <= 0 || patientId <= 0)
                {
                    TempData["ErrorMessage"] = "Invalid slot or patient selection.";
                    return RedirectToAction(nameof(TodayAppointments));
                }

                int token = _receptionistService.BookAppointment(slotId, patientId);

                // Optional: if SP returns no token, still show success
                if (token <= 0)
                    TempData["SuccessMessage"] = "Appointment booked successfully!";
                else
                    TempData["SuccessMessage"] = $"Appointment booked successfully. Token Number: {token}";

                return RedirectToAction(nameof(TodayAppointments));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(TodayAppointments));
            }
        }



        // Delete Patient (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePatient(int id)
        {
            _receptionistService.DeletePatient(id);
            TempData["SuccessMessage"] = "Patient deleted successfully!";
            return RedirectToAction(nameof(Patients));
        }

        // Billing Page
        [HttpPost]
        public IActionResult GenerateConsultationBill(int patientId, int appointmentId, decimal consultationFee)
        {
            Console.WriteLine($"Generating bill for Patient={patientId}, Appointment={appointmentId}, Fee={consultationFee}");

            bool success = _receptionistService.GenerateConsultationBill(patientId, appointmentId, consultationFee);

            if (success)
            {
                TempData["Message"] = "Consultation bill generated successfully!";
                return RedirectToAction("ViewConsultationBill", new { patientId, appointmentId });
            }
            else
            {
                TempData["Error"] = "Failed to generate consultation bill.";
                return View("GenerateConsultationBill");
            }
        }


    }
}