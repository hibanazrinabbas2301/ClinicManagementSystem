using ClinicManagementSystem.Repositories;
using ClinicManagementSystem.Services;

namespace ClinicManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            // ? Dependency Injection
            builder.Services.AddScoped<IUserRepo, UserRepoImpl>();
            builder.Services.AddScoped<IUserService, UserServiceImpl>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepositoryImpl>();
            builder.Services.AddScoped<IDoctorService, DoctorServiceImpl>();


            // ? Enable Session Properly
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

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
