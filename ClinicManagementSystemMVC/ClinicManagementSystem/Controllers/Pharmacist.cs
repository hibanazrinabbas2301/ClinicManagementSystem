using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class Pharmacist : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
