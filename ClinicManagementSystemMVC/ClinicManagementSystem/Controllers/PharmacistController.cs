using ClinicManagementSystem.Security;
using ClinicManagementSystem.Services;
using ClinicManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;


namespace ClinicManagementSystem.Controllers
{
    [RoleAuthorize("Pharmacist")]
    public class PharmacistController : Controller
    {
        private readonly IpharmacistService _pharmacistService;

        public PharmacistController(IpharmacistService pharmacistService)
        {
            _pharmacistService = pharmacistService;
        }

        // ======================================================
        // ✅ Dashboard - Medicine List
        // ======================================================
        [HttpGet]
        public IActionResult Index()
        {
            var medicines = _pharmacistService.GetAllMedicines();
            return View(medicines);

        }

        // ======================================================
        // ✅ Add Medicine (AJAX)
        // ======================================================
        [HttpPost]
        public JsonResult AddMedicine(AddMedicineViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        k => k.Key,
                        v => v.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    );

                return Json(new { success = false, errors });
            }


            try
            {
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
            catch (Exception ex)
            {
                return Json(new { Success = true });

            }
        }

        // ======================================================
        // ✅ Get Categories (Dropdown)
        // ======================================================
        [HttpGet]
        public JsonResult GetCategories()
        {
            var categories = _pharmacistService.GetCategories();
            return Json(categories);
        }

        // ======================================================
        // ✅ Update Medicine (Stock / Price / Expiry)
        // ======================================================
        [HttpPost]
        public JsonResult UpdateMedicine(UpdateMedicineViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Json(new
            //    {
            //        success = false,
            //        message = "Invalid data. Please check the form."
            //    });
            //}

            //try
            //{
            //    _pharmacistService.UpdateMedicine(model);

            //    return Json(new
            //    {
            //        success = true,
            //        message = "Medicine updated successfully!",
            //        name = model.MedicineName,
            //        description = model.MedicineDescription,
            //        addedStock = model.AddedQuantity,
            //        price = model.NewPrice,
            //        expiry = model.NewExpiry?.ToString("dd-MM-yyyy")
            //    });
            //}
            //catch (Exception)
            //{
            //    return Json(new
            //    {
            //        success = false,
            //        message = "Update failed. Please try again."
            //    });
            //}
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { success = false, errors });
            }

            _pharmacistService.UpdateMedicine(model);

            return Json(new { success = true });
        }
    }
}