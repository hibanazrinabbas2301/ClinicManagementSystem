using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ClinicManagementSystem.Controllers
{
    public class TestController : Controller
    {
        private readonly IConfiguration _configuration;

        public TestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            string connectionString = _configuration.GetConnectionString("ConnStringMVC");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();

                    SqlCommand command = new SqlCommand("select DB_NAME() AS DatabaseName,@@SERVERNAME As ServerName FROM INFORMATION_SCHEMA.TABLES", connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            ViewBag.Message = "Connection Suceesful";
                        }
                    }

                    ViewBag.Message = "Connection successful";

                }
            }



            catch (SqlException ex)
            {
                // Handle other exceptions
                ViewBag.Message = "Connection failed";
                ViewBag.Error = ex.Message;
            }

            return View();
        }
    }
}