

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
        public IActionResult Index(AddMedicineViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }

            int newId = _pharmacistService.AddMedicine(model);

            var category = _pharmacistService
                    .GetCategories()
                    .FirstOrDefault(c => c.CategoryId == model.CategoryId);


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


        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _pharmacistService.GetCategories();
            return Json(categories);
        }

        //edit medcine stock
        [HttpPost]
        public IActionResult UpdateMedicine(UpdateMedicineViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false });

            try
            {
                _pharmacistService.UpdateMedicine(model);

                return Json(new
                {
                    success = true,
                    message="Medicine updated successfully",
                    name = model.MedicineName,
                    description = model.MedicineDescription,
                    addedStock = model.AddedQuantity,
                    price = model.NewPrice,
                    expiry = model.NewExpiry
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    success = false,
                    message = "Update failed. Please try again."
                });
            }
        }




    }
}
