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

        // GET: Login Page
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            var user = _service.AuthenticateLogin(model.UserName, model.UserPassword);

            if (user == null)
            {
                ViewBag.Error = "Invalid Username or Password!";
                return View();
            }

            // Store Session
            HttpContext.Session.SetInt32("StaffId", user.StaffId);
            HttpContext.Session.SetString("RoleName", user.RoleName);
            HttpContext.Session.SetString("UserName", user.Name);

            // Redirect Role Dashboard
            if (user.RoleName == "Receptionist")
                return RedirectToAction("Index", "Receptionist");

            if (user.RoleName == "Doctor")
                return RedirectToAction("Index", "Doctor");

            if (user.RoleName == "Pharmacist")
                return RedirectToAction("Index", "Pharmacist");

            if (user.RoleName == "LabTech")
                return RedirectToAction("Index", "Lab");

            return RedirectToAction("Login");
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
