using ClinicManagementSystem.Models;
using ClinicManagementSystem.Security;
using ClinicManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    [RoleAuthorize("Doctor")]

    public class DoctorController : Controller
    {
        private readonly IDoctorService _service;

        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        // ===============================
        // ✅ 1. Doctor Dashboard
        // ===============================
        public IActionResult Index()
        {

            int doctorId = Convert.ToInt32(HttpContext.Session.GetInt32("DoctorId"));

            var appointments = _service.GetTodaysAppointments(doctorId);

            return View(appointments);
        }

        // ===============================
        // ✅ 2. Consultation Page (Per Patient)
        // ===============================
        public IActionResult Consultation(int appointmentId, int patientId)
        {
            // Dropdown Data
            ViewBag.Medicines = _service.GetMedicines();
            ViewBag.LabTests = _service.GetLabTests();

            // Prescriptions Already Added
            ViewBag.MedicineList = _service.GetMedicinesByAppointment(appointmentId);
            ViewBag.LabList = _service.GetLabTestsByAppointment(appointmentId);

            // Patient History
            ViewBag.History = _service.GetPatientHistory(patientId);
            ViewBag.PatientInfo = _service.GetPatientBasicDetails(patientId);
            // ✅ Patient Diagnosis History
            ViewBag.History = _service.GetPatientHistory(patientId);

            // ✅ NEW: Medicine + Lab History
            ViewBag.MedicineHistory = _service.GetPatientMedicineHistory(patientId);
            ViewBag.LabHistory = _service.GetPatientLabHistory(patientId);


            // ✅ Load Existing Diagnosis (if already saved)
            Diagnosis model = _service.GetDiagnosisByAppointment(appointmentId);

            // If no diagnosis exists yet → create empty model
            if (model.AppointmentId == 0)
            {
                model = new Diagnosis()
                {
                    AppointmentId = appointmentId,
                    PatientId = patientId
                };
            }

            return View(model);
        }

        // ===============================
        // ✅ 3. Save Diagnosis Draft
        // ===============================
        [HttpPost]
        public IActionResult AddDiagnosis(Diagnosis model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "⚠ Please fill all Diagnosis fields!";
                return RedirectToAction("Consultation",
                    new { appointmentId = model.AppointmentId, patientId = model.PatientId });
            }

            model.DoctorId = Convert.ToInt32(HttpContext.Session.GetInt32("StaffId"));

            _service.AddDiagnosis(model);

            TempData["Success"] = "✅ Diagnosis Saved Successfully!";

            return RedirectToAction("Consultation",
                new { appointmentId = model.AppointmentId, patientId = model.PatientId });
        }

        // ===============================
        // ✅ 4. Add Medicine (Multiple Allowed)
        // ===============================
        [HttpPost]
        public IActionResult AddMedicinePrescription(MedicinePrescription model)
        {
            if (model.MedicineId == 0 || model.Quantity <= 0 || string.IsNullOrEmpty(model.Dosage))
            {
                TempData["Error"] = "⚠ Please select medicine, quantity and dosage!";
                return RedirectToAction("Consultation",
                    new { appointmentId = model.AppointmentId, patientId = model.PatientId });
            }


            model.DoctorId = Convert.ToInt32(HttpContext.Session.GetInt32("StaffId"));

            _service.AddMedicinePrescription(model);

            TempData["Success"] = "💊 Medicine Added Successfully!";

            return RedirectToAction("Consultation",
                new { appointmentId = model.AppointmentId, patientId = model.PatientId });
        }

        // ===============================
        // ✅ 5. Add Lab Test (Multiple Allowed)
        // ===============================
        [HttpPost]
        public IActionResult AddLabPrescription(LabPrescription model)
        {
            if (model.TestId == null || model.TestId == 0)
            {
                // No error, just return back
                return RedirectToAction("Consultation",
                    new { appointmentId = model.AppointmentId, patientId = model.PatientId });
            }

            model.DoctorId = Convert.ToInt32(HttpContext.Session.GetInt32("StaffId"));
            model.Quantity = 1;


            _service.AddLabPrescription(model);

            TempData["Success"] = "🧪 Lab Test Added Successfully!";

            return RedirectToAction("Consultation",
                new { appointmentId = model.AppointmentId, patientId = model.PatientId });
        }

        // ===============================
        // ✅ 6. FINAL COMPLETE CONSULTATION
        // ===============================
        [HttpPost]
        public IActionResult FinalSaveConsultation(int appointmentId, int patientId)
        {
            // ✅ Mark appointment completed
            _service.MarkAppointmentCompleted(appointmentId);

            TempData["Success"] = "✅ Consultation Completed Successfully!";

            return RedirectToAction("Index");
        }
    }
}
