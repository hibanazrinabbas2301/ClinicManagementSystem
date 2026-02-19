using ClinicManagementSystem.Services;
using ClinicManagementSystem.View_Model;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _service;

        public LoginController(IUserService service)
        {
            _service = service;
        }

        // ✅ GET: Login Page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserModel model)
        {
            // ✅ Step 1: Required field validation
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // ✅ Step 2: Authenticate user
            var user = _service.AuthenticateLogin(model.UserName, model.UserPassword);

            // ❌ Wrong credentials
            if (user == null)
            {
                ViewBag.Error = "❌ Wrong Username or Password!";
                return View(model);   // ✅ Stay on login page
            }

            // ✅ Step 3: Store Session
            HttpContext.Session.SetInt32("StaffId", user.StaffId);
            HttpContext.Session.SetString("RoleName", user.RoleName);
            HttpContext.Session.SetString("UserName", user.Name);
            if (user.RoleName == "Doctor" && user.DoctorId != null)
            {
                HttpContext.Session.SetInt32("DoctorId", user.DoctorId.Value);
            }

            // ✅ Step 4: Redirect Role Dashboard
            if (user.RoleName == "Receptionist")
                return RedirectToAction("Index", "Receptionist");

            if (user.RoleName == "Doctor")
                return RedirectToAction("Index", "Doctor");

            if (user.RoleName == "Pharmacist")
                return RedirectToAction("Index", "Home");

            if (user.RoleName == "Lab Technician")
                return RedirectToAction("LabTechDashBoard", "LabTechnicians");

            return RedirectToAction("Index");
        }
    


        // ✅ Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            return RedirectToAction("Index","Login"); // ✅ Fix
        }
    }
}
