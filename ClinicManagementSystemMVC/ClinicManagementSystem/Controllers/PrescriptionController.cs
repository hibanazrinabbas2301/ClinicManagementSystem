using ClinicManagementSystem.Security;
using ClinicManagementSystem.Services;
using ClinicManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class PrescriptionController : Controller
    {

        private readonly IpharmacistService _pharmacistService;

        public PrescriptionController(IpharmacistService pharmacistService)
        {
            _pharmacistService = pharmacistService;
        }

        // Pending list
        public IActionResult Index()
        {
            

            var data = _pharmacistService.GetPendingAppointments();
            return View(data);
        }

        // Get multiple medicines
        public IActionResult GetPrescriptionDetails(int appointmentId)
        {
            var data = _pharmacistService.GetPrescriptionDetails(appointmentId);
            return Json(data);
        }

        // Issue
        [HttpPost]
        public IActionResult Issue(int prescriptionId)
        {
            var result = _pharmacistService.IssuePrescription(prescriptionId);

            if (result == "SUCCESS")
                return Json(new { success = true });

            if (result == "OUT_OF_STOCK")
                return Json(new { success = false, message = "Out of Stock! Insufficient medicine quantity." });

            return Json(new { success = false, message = "Something went wrong. Please try again." });
        }




        // Issued medicines list page
        public IActionResult IssuedBills()
        {
            var data = _pharmacistService.GetIssuedMedicinesBill();
            return View(data);
        }

        // PDF generation
        public IActionResult GenerateBillPdf(int appointmentId)
        {
            var data = _pharmacistService.GetIssuedMedicinesBill()
                       .Where(x => x.AppointmentId == appointmentId)
                       .ToList();

            return View("BillPdf", data);
        }
    }



}
