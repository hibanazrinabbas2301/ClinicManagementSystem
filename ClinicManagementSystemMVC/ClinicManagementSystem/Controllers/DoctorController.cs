using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _service;

        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        // =====================================================
        // ✅ 1. Doctor Dashboard (Today's Appointments)
        // =====================================================
        public IActionResult Index()
        {
            int doctorId = Convert.ToInt32(HttpContext.Session.GetInt32("StaffId"));

            var appointments = _service.GetTodaysAppointments(doctorId);

            return View(appointments);
        }

        // =====================================================
        // ✅ 2. Consultation Page (Per Patient)
        // =====================================================
        public IActionResult Consultation(int appointmentId, int patientId)
        {
            // Dropdown Data
            ViewBag.Medicines = _service.GetMedicines();
            ViewBag.LabTests = _service.GetLabTests();

            // Already Added Medicines & Lab Tests
            ViewBag.MedicineList = _service.GetMedicinesByAppointment(appointmentId);
            ViewBag.LabList = _service.GetLabTestsByAppointment(appointmentId);

            // Patient Consultation History
            ViewBag.History = _service.GetPatientHistory(patientId);

            // Diagnosis Model for Form Binding
            Diagnosis model = new Diagnosis()
            {
                AppointmentId = appointmentId,
                PatientId = patientId
            };

            return View(model);
        }

        // =====================================================
        // ✅ 3. Save Diagnosis + Consultation Notes
        // =====================================================
        [HttpPost]
        public IActionResult AddDiagnosis(Diagnosis model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill consultation details properly!";
                return RedirectToAction("Consultation",
                    new { appointmentId = model.AppointmentId, patientId = model.PatientId });
            }

            model.DoctorId = Convert.ToInt32(HttpContext.Session.GetInt32("StaffId"));

            _service.AddDiagnosis(model);

            TempData["Success"] = "✅ Consultation Details Saved Successfully!";

            return RedirectToAction("Consultation",
                new { appointmentId = model.AppointmentId, patientId = model.PatientId });
        }

        // =====================================================
        // ✅ 4. Add Medicine Prescription (Multiple Allowed)
        // =====================================================
        [HttpPost]
        public IActionResult AddMedicinePrescription(MedicinePrescription model)
        {
            if (model.MedicineId == 0 || model.Quantity <= 0)
            {
                TempData["Error"] = "⚠ Please select medicine and enter quantity!";
                return RedirectToAction("Consultation",
                    new { appointmentId = model.AppointmentId, patientId = model.PatientId });
            }

            model.DoctorId = Convert.ToInt32(HttpContext.Session.GetInt32("StaffId"));

            _service.AddMedicinePrescription(model);

            TempData["Success"] = "💊 Medicine Added Successfully!";

            return RedirectToAction("Consultation",
                new { appointmentId = model.AppointmentId, patientId = model.PatientId });
        }

        // =====================================================
        // ✅ 5. Add Lab Test Prescription (Multiple Allowed)
        // =====================================================
        [HttpPost]
        public IActionResult AddLabPrescription(LabPrescription model)
        {
            if (model.TestId == 0 || model.Quantity <= 0)
            {
                TempData["Error"] = "⚠ Please select lab test and enter quantity!";
                return RedirectToAction("Consultation",
                    new { appointmentId = model.AppointmentId, patientId = model.PatientId });
            }

            model.DoctorId = Convert.ToInt32(HttpContext.Session.GetInt32("StaffId"));

            _service.AddLabPrescription(model);

            TempData["Success"] = "🧪 Lab Test Added Successfully!";

            return RedirectToAction("Consultation",
                new { appointmentId = model.AppointmentId, patientId = model.PatientId });
        }
        [HttpPost]
        public IActionResult FinalSaveConsultation(Diagnosis model)
        {
            model.DoctorId = Convert.ToInt32(HttpContext.Session.GetInt32("StaffId"));

            // 1. Save Diagnosis
            _service.AddDiagnosis(model);

            // 2. Mark Appointment Completed
            _service.MarkAppointmentCompleted(model.AppointmentId);

            TempData["Success"] = "Consultation Completed Successfully!";

            return RedirectToAction("Index");
        }


    }
}
