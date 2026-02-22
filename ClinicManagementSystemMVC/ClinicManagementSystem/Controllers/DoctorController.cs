using ClinicManagementSystem.Models;
using ClinicManagementSystem.Security;
using ClinicManagementSystem.Services;
using ClinicManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            ViewBag.LabResults = _service.GetLabResultsByAppointment(appointmentId);


            // ✅ Load Existing Diagnosis (if already saved)
            Diagnosis model = _service.GetDiagnosisByAppointment(appointmentId);

            // If no diagnosis exists yet → create empty model
            //if (model.AppointmentId == 0)
            //{
            //    model = new Diagnosis()
            //    {
            //        AppointmentId = appointmentId,
            //        PatientId = patientId
            //    };
            //}
            model.AppointmentId = appointmentId;
            model.PatientId = patientId;
            ViewBag.FrequencyList = new List<SelectListItem>
{
    new SelectListItem { Text = "Once Daily", Value = "1" },
    new SelectListItem { Text = "Twice Daily", Value = "2" },
    new SelectListItem { Text = "Three Times Daily", Value = "3" },
    new SelectListItem { Text = "Four Times Daily", Value = "4" }
};

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

            model.DoctorId = Convert.ToInt32(HttpContext.Session.GetInt32("DoctorId"));

            _service.AddDiagnosis(model);

            TempData["Success"] = "✅ Diagnosis Saved Successfully!";

            return RedirectToAction("Consultation",
                new { appointmentId = model.AppointmentId, patientId = model.PatientId });
        }

        // ===============================
        // ✅ 4. Add Medicine (Multiple Allowed)
        // ===============================
        [HttpPost]
        public IActionResult AddMedicinePrescription(
    [FromForm] int AppointmentId,
    [FromForm] int PatientId,
    [FromForm] int MedicineId,
    [FromForm] string Dosage,
    [FromForm] int Frequency,
    [FromForm] int DurationDays)
        {
            if (AppointmentId <= 0)
            {
                TempData["Error"] = "Invalid Appointment ID: " + AppointmentId;
                return RedirectToAction("Index");
            }

            var model = new PrescriptionDetailViewModel
            {
                AppointmentId = AppointmentId,
                PatientId = PatientId,
                DoctorId = Convert.ToInt32(HttpContext.Session.GetInt32("DoctorId")),
                MedicineId = MedicineId,
                Dosage = Dosage,
                Frequency = Frequency,
                DurationDays = DurationDays
            };

            _service.AddMedicinePrescription(model);

            return RedirectToAction("Consultation",
                new { appointmentId = AppointmentId, patientId = PatientId });
        }

        //    try
        //    {
        //        var model = new PrescriptionDetailViewModel
        //        {
        //            AppointmentId = AppointmentId,
        //            PatientId = PatientId,
        //            DoctorId = Convert.ToInt32(HttpContext.Session.GetInt32("DoctorId")),
        //            MedicineId = MedicineId,
        //            Dosage = Dosage,
        //            Frequency = Frequency,
        //            DurationDays = DurationDays
        //        };

        //        _service.AddMedicinePrescription(model);

        //        TempData["Success"] = "💊 Medicine added successfully!";
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error"] = ex.Message;
        //    }

        //    return RedirectToAction("Consultation",
        //        new { appointmentId = AppointmentId, patientId = PatientId });
        //}

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

            model.DoctorId = Convert.ToInt32(HttpContext.Session.GetInt32("DoctorId"));
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
