using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ClinicManagementSystem.Controllers
{
    public class LabTechniciansController : Controller
    {
        private readonly ILabTechnicianService _labTechnicianService;

        public LabTechniciansController(ILabTechnicianService labTechnicianService)
        {
            _labTechnicianService = labTechnicianService;
        }

        #region Dashboard

        public IActionResult LabTechDashboard()
        {
            ViewBag.PendingTests = _labTechnicianService.SelectPendingTests();
            var labTests = _labTechnicianService.SelectAllLabTests().ToList();
            return View(labTests);
        }

        #endregion


        #region Index

        public ActionResult Index()
        {
            ViewBag.PendingTests = _labTechnicianService.SelectPendingTests();
            ViewBag.CompletedTests = _labTechnicianService.SelectCompletedTests();

            var labTests = _labTechnicianService.SelectAllLabTests().ToList();
            return View(labTests);
        }


        #endregion


        #region Add Lab Test

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LabTest labTest)
        {
            if (ModelState.IsValid)
            {
                _labTechnicianService.InsertLabTest(labTest);
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        #endregion


        #region Get Lab Test By Id

        [HttpGet]
        public JsonResult GetLabTestById(int id)
        {
            var labTest = _labTechnicianService.SelectLabTestById(id);

            if (labTest == null)
                return Json(new { success = false, message = "Lab Test not found." });

            return Json(new { success = true, labTest });
        }

        #endregion


        #region Edit Lab Test

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(LabTest labTest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                return Json(new { success = false, errors });
            }

            _labTechnicianService.UpdateLabTest(labTest);
            return Json(new { success = true, message = "Lab Test updated successfully!" });
        }

        #endregion


        #region Add Lab Result

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddLabResult(LabResult result, int PrescriptionId)
        {
            if (ModelState.IsValid)
            {
                // 1️⃣ Save result & get ID
                int resultId = _labTechnicianService.InsertLabResult(result);

                // 2️⃣ Mark completed
                _labTechnicianService.UpdateLabTestStatus(PrescriptionId, "Completed");

                // 3️⃣ Auto calculate bill (use test price as bill)
                decimal amount = result.ActualValue; // Or pass Price from UI
                _labTechnicianService.CreateLabBill(resultId, amount);

                return Json(new { success = true, resultId, amount });
            }

            return Json(new { success = false });
        }


        #endregion


        #region Update Test Status

        [HttpPost]
        public JsonResult UpdateTestStatus(int prescriptionId, string status)
        {
            _labTechnicianService.UpdateLabTestStatus(prescriptionId, status);
            return Json(new { success = true });
        }

        #endregion


        #region Generate Lab Bill

        [HttpPost]
        public JsonResult GenerateLabBill(int resultId, decimal amount)
        {
            _labTechnicianService.CreateLabBill(resultId, amount);
            return Json(new { success = true, message = "Lab bill generated successfully!" });
        }

        #endregion


        #region Print PDF Bill (Simple Download)

        [HttpGet]
        public IActionResult PrintLabBill(int resultId)
        {
            var result = _labTechnicianService.GetResultById(resultId);

            if (result == null)
                return NotFound();

            // 🔹 Get Test Info
            var test = _labTechnicianService
                            .SelectAllLabTests()
                            .FirstOrDefault(t => t.TestId == result.TestId);

            decimal billAmount = test?.Price ?? 0;

            var html = $@"
        <html>
        <head>
            <style>
                body {{ font-family: Arial; padding: 30px; }}
                h2 {{ color: #2c3e50; }}
                table {{ width: 100%; border-collapse: collapse; margin-top:20px; }}
                th, td {{ border:1px solid #ddd; padding:8px; text-align:left; }}
                th {{ background-color:#f4f4f4; }}
                .total {{ font-weight:bold; font-size:18px; }}
                .header {{ text-align:center; margin-bottom:30px; }}
            </style>
        </head>
        <body>

            <div class='header'>
                <h2>Clinic Management System</h2>
                <p>Official Lab Report</p>
            </div>

            <table>
                <tr><th>Result ID</th><td>{result.ResultId}</td></tr>
                <tr><th>Test ID</th><td>{result.TestId}</td></tr>
                <tr><th>Patient ID</th><td>{result.PatientId}</td></tr>
                <tr><th>Normal Range</th><td>{result.NormalRange}</td></tr>
                <tr><th>Actual Value</th><td>{result.ActualValue}</td></tr>
                <tr><th>Remarks</th><td>{result.Remarks}</td></tr>
                <tr><th>Date</th><td>{result.Date}</td></tr>
            </table>

            <br/>

            <table>
                <tr>
                    <th>Lab Bill Amount</th>
                    <td class='total'>₹ {billAmount}</td>
                </tr>
            </table>

            <br/>
            <p>Doctor Review: {result.DoctorReview}</p>

            <hr/>
            <p style='text-align:center;'>Thank you for choosing our clinic.</p>

        </body>
        </html>
    ";

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(html);

            return File(bytes, "application/pdf", $"LabReport_{result.ResultId}.pdf");
        }


        #endregion


        #region Email Report To Patient

        [HttpPost]
        public JsonResult EmailReport(int resultId, string patientEmail)
        {
            try
            {
                var result = _labTechnicianService.GetResultById(resultId);

                if (result == null)
                    return Json(new { success = false, message = "Result not found." });

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("yourclinicemail@gmail.com");
                mail.To.Add(patientEmail);
                mail.Subject = "Your Lab Test Report";

                mail.Body = $@"
                Lab Test Report

                Result ID: {result.ResultId}
                Test ID: {result.TestId}
                Normal Range: {result.NormalRange}
                Actual Value: {result.ActualValue}
                Remarks: {result.Remarks}
                Date: {result.Date}
    
                Thank you.
                Clinic Management System
                ";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("yourclinicemail@gmail.com", "your-app-password");
                smtp.EnableSsl = true;

                smtp.Send(mail);

                return Json(new { success = true, message = "Email sent successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion


        #region Patient Reports

        [HttpGet]
        public IActionResult PatientReports(int patientId)
        {
            var reports = _labTechnicianService.SelectPatientLabReports(patientId).ToList();
            return View(reports);
        }

        #endregion


        #region Get Lab Report Details (For Modal)

        [HttpGet]
        public JsonResult GetLabReportDetails(int resultId)
        {
            var result = _labTechnicianService.GetResultById(resultId);

            if (result == null)
                return Json(new { success = false });

            var test = _labTechnicianService
                        .SelectAllLabTests()
                        .FirstOrDefault(t => t.TestId == result.TestId);

            var prescription = _labTechnicianService
                               .SelectCompletedTests()
                               .FirstOrDefault(p => p.ResultId == resultId);

            return Json(new
            {
                success = true,
                prescriptionId = prescription?.PrescriptionId,
                patientName = prescription?.PatientName,
                doctorName = prescription?.DoctorName,
                testName = prescription?.TestName,
                normalRange = prescription?.NormalRange,
                actualValue = result.ActualValue,
                remarks = result.Remarks,
                bill = test?.Price,
                date = result.Date.ToString("dd/MM/yyyy")
            });
        }


        #endregion

    }
}
