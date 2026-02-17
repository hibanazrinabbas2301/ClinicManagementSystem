using ClinicManagementSystem.Models;
using ClinicManagementSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class LabTechniciansController : Controller
    {
        private readonly ILabTechnicianService _labTechnicianService;

        // DI
        public LabTechniciansController(ILabTechnicianService labTechnicianService)
        {
            _labTechnicianService = labTechnicianService;
        }

        #region Index (Lab Tests + Pending Tests)

        public ActionResult Index()
        {
            ViewBag.PendingTests = _labTechnicianService.SelectPendingTests();

            var labTests = _labTechnicianService.SelectAllLabTests().ToList();
            return View(labTests);
        }

        #endregion


        #region Add Lab Test

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LabTest labTest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _labTechnicianService.InsertLabTest(labTest);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        #endregion


        #region Get Lab Test By Id (For Edit Modal)

        [HttpGet]
        public JsonResult GetLabTestById(int id)
        {
            var labTest = _labTechnicianService.SelectLabTestById(id);

            if (labTest == null)
            {
                return Json(new { success = false, message = "Lab Test not found." });
            }

            return Json(new { success = true, labTest });
        }

        #endregion


        #region Edit Lab Test

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(LabTest labTest)
        {
            if (labTest == null)
                return Json(new { success = false, message = "Invalid data." });

            if (ModelState.IsValid)
            {
                _labTechnicianService.UpdateLabTest(labTest);
                return Json(new { success = true, message = "Lab Test updated successfully!" });
            }

            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);

            return Json(new { success = false, errors });
        }

        #endregion


        #region View Pending Tests

        public ActionResult PendingTests()
        {
            var pendingTests = _labTechnicianService.SelectPendingTests().ToList();
            return View(pendingTests);
        }

        #endregion


        #region Add Lab Result

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddLabResult(LabResult result)
        {
            if (result == null)
                return Json(new { success = false, message = "Invalid data." });

            if (ModelState.IsValid)
            {
                _labTechnicianService.InsertLabResult(result);

                return Json(new { success = true, message = "Lab Result added successfully!" });
            }

            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);

            return Json(new { success = false, errors });
        }

        #endregion


        #region Update Test Status

        [HttpPost]
        public JsonResult UpdateTestStatus(int prescriptionId, string status)
        {
            try
            {
                _labTechnicianService.UpdateLabTestStatus(prescriptionId, status);
                return Json(new { success = true, message = "Test status updated!" });
            }
            catch
            {
                return Json(new { success = false, message = "Error updating status." });
            }
        }

        #endregion


        #region Generate Lab Bill

        [HttpPost]
        public JsonResult GenerateLabBill(int resultId, decimal amount)
        {
            try
            {
                _labTechnicianService.CreateLabBill(resultId, amount);
                return Json(new { success = true, message = "Lab bill generated successfully!" });
            }
            catch
            {
                return Json(new { success = false, message = "Error generating bill." });
            }
        }

        #endregion


        #region View Patient Lab Reports

        [HttpGet]
        public ActionResult PatientReports(int patientId)
        {
            var reports = _labTechnicianService.SelectPatientLabReports(patientId).ToList();
            return View(reports);
        }

        #endregion
    }
}
