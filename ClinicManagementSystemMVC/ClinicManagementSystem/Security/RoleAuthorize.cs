using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClinicManagementSystem.Security
{
    public class RoleAuthorize : ActionFilterAttribute
    {
        private readonly string _role;

        public RoleAuthorize(string role)
        {
            _role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // ✅ Prevent browser caching (important for Back button)
            context.HttpContext.Response.Headers["Cache-Control"] =
                "no-cache, no-store, must-revalidate";
            context.HttpContext.Response.Headers["Pragma"] = "no-cache";
            context.HttpContext.Response.Headers["Expires"] = "0";

            // ✅ Check Session Exists
            var staffId = context.HttpContext.Session.GetInt32("StaffId");
            var role = context.HttpContext.Session.GetString("RoleName");

            // ✅ If user is logged out → block access immediately
            if (staffId == null || role == null)
            {
                context.Result = new RedirectToActionResult(
                    "Index",   // Login page action
                    "Login",   // LoginController
                    null
                );
                return;
            }

            // ✅ Role mismatch → block access
            if (role != _role)
            {
                context.Result = new RedirectToActionResult(
                    "Index",
                    "Login",
                    null
                );
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
