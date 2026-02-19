using ClinicManagementSystem.Repositories;
using ClinicManagementSystem.Service;
using ClinicManagementSystem.Services;
using ClinicManagementSystem_Final.Repository;

namespace ClinicManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            // ? Enable Session Properly
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddScoped<ILabTechnicianRepository, LabTechnicianRepository>();
            builder.Services.AddScoped<ILabTechnicianService, LabTechnicianService>();


            // ? Dependency Injection
            builder.Services.AddScoped<IUserRepo, UserRepoImpl>();
            builder.Services.AddScoped<IUserService, UserServiceImpl>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepositoryImpl>();
            builder.Services.AddScoped<IDoctorService, DoctorServiceImpl>();
            // Register Receptionist Repository
            builder.Services.AddScoped<IReceptionistRepository, ReceptionistRepositoryImpl>();
            //Register Receptionist Service
            builder.Services.AddScoped<IReceptionistService, ReceptionistServiceImpl>();
            builder.Services.AddScoped<IEmailService, SmtpEmailService>();

            builder.Services.AddScoped<IPharmacistRepo, PharmacistRepoImpl>();
            builder.Services.AddScoped<IpharmacistService, PharmacistServiceImpl>();


            builder.Services.AddScoped<IPharmacistRepo, PharmacistRepoImpl>();
            builder.Services.AddScoped<IpharmacistService, PharmacistServiceImpl>();



            var app = builder.Build();


            
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // ? Session MUST be here
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
