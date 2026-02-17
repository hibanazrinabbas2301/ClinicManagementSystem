using ClinicManagementSystem.Services;
using ClinicManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly IpharmacistService _pharmacistService;

        // Dependency Injection Constructor
        public PrescriptionController(IpharmacistService pharmacistService)
        {
            _pharmacistService = pharmacistService;
        }

        public IActionResult Index()
        {
            var data = _pharmacistService.GetPendingPrescriptions();

            var grouped = data
                .GroupBy(x => x.PrescriptionId)
                .Select(g => new PendingPrescriptionViewModel
                {
                    PrescriptionId = g.Key,
                    PatientName = g.First().PatientName,
                    PrescribedDate = g.First().PrescribedDate
                }).ToList();

            return View(grouped);
        }




        [HttpGet]
        public IActionResult GetPrescriptionDetails(int id)
        {
            var data = _pharmacistService.GetPrescriptionDetailsById(id);

            return Json(data);
        }




        [HttpPost]
        public IActionResult Issue(int prescriptionId)
        {
            var result = _pharmacistService.IssuePrescription(prescriptionId);

            if (result == 1)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Stock not available" });
        }


    }


}
