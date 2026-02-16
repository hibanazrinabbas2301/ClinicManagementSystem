

using ClinicManagementSystem.Service;
using ClinicManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class PharmacistController : Controller
    {
        private readonly IpharmacistService _pharmacistService;

        public PharmacistController(IpharmacistService pharmacistService)
        {
            _pharmacistService = pharmacistService;
        }

        public IActionResult Index()
        {
            var medicines = _pharmacistService.GetAllMedicines();
            return View(medicines);
        }


        [HttpPost]
        [HttpPost]
        public IActionResult Index(AddMedicineViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }

            int newId = _pharmacistService.AddMedicine(model);

            return Json(new
            {
                success = true,
                id = newId,
                name = model.MedicineName,
                description = model.Description,
                stock = model.Quantity,
                expiry = model.ExpiryDate.ToString("dd-MM-yyyy"),
                price = model.Price
            });
        }


    }
}
