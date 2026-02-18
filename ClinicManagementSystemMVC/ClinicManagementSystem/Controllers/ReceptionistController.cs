using ClinicManagementSystem.Models;
using ClinicManagementSystem.Security;
using ClinicManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem_Final.Controllers
{
    [RoleAuthorize("Receptionist")]

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

        //// Add new patient (GET)
        //public IActionResult AddPatient()
        //{
        //    return View();
        //}

        // Add new patient (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the errors and try again.";

                var patients = _receptionistService.GetAllPatients();
                return View("Patients", patients); // show same page WITH errors
            }

            _receptionistService.AddPatient(patient);
            TempData["SuccessMessage"] = "Patient added successfully!";
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
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the errors and try again.";
                var patients = _receptionistService.GetAllPatients();
                return View("Patients", patients);
            }

            _receptionistService.UpdatePatient(patient);
            TempData["SuccessMessage"] = "Patient updated successfully!";
            return RedirectToAction(nameof(Patients));
        }

        // ---------------- Appointments ----------------

        // GET: View today's appointments
        [HttpGet]
        public IActionResult TodayAppointments(DateTime? slotDate)
        {
            var date = slotDate ?? DateTime.Today;   

            ViewBag.SelectedDate = date.ToString("yyyy-MM-dd");

            ViewBag.Patients = _receptionistService.GetAllPatients() ?? new List<Patient>();

            ViewBag.Slots = _receptionistService.GetAvailableDoctorSlots(date) ?? new List<SlotViewModel>();

            var appointments = _receptionistService.GetAppointmentsByDate(date) ?? new List<AppointmentViewModel>();

            return View(appointments);
        }

        // POST: Book new appointment from modal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookAppointment(int slotId, int patientId, DateTime slotDate)
        {
            try
            {
                if (slotId <= 0 || patientId <= 0)
                {
                    TempData["ErrorMessage"] = "Invalid slot or patient selection.";
                    return RedirectToAction(nameof(TodayAppointments), new { slotDate = slotDate.ToString("yyyy-MM-dd") });
                }

                int token = _receptionistService.BookAppointment(slotId, patientId);

                TempData["SuccessMessage"] = token <= 0
                    ? "Appointment booked successfully!"
                    : $"Appointment booked successfully. Token Number: {token}";

                // ✅ reload same date that was booked
                return RedirectToAction(nameof(TodayAppointments), new { slotDate = slotDate.ToString("yyyy-MM-dd") });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(TodayAppointments), new { slotDate = slotDate.ToString("yyyy-MM-dd") });
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
            // Ignore consultationFee for now because SP calculates from Doctor table
            bool success = _receptionistService.GenerateConsultationBill(appointmentId);
            if (!success)
            {
                TempData["ErrorMessage"] = "Failed to generate consultation bill.";
                return RedirectToAction(nameof(TodayAppointments));
            }

            return RedirectToAction(nameof(ViewConsultationBill), new { appointmentId });
        }

        [HttpGet]
        public IActionResult ViewConsultationBill(int appointmentId)
        {
            var bill = _receptionistService.GetConsultationBillDetails(appointmentId);
            if (bill == null)
            {
                TempData["ErrorMessage"] = "Bill not found for this appointment.";
                return RedirectToAction(nameof(TodayAppointments));
            }

            return View(bill); // must have Views/Receptionist/ViewConsultationBill.cshtml
        }

        [HttpGet]
        public JsonResult GetSlotsByDate(DateTime slotDate)
        {
            var slots = _receptionistService.GetAvailableDoctorSlots(slotDate);

            var data = slots.Select(s => new {
                slotId = s.SlotId,
                text = s.DisplayText   // or build from DoctorName + Start-End
            });

            return Json(data);
        }



    }
}