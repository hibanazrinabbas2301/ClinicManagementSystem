using _2026_EMS_Project_new_Batch.Repository;
using _2026_EMS_Project_new_Batch.Service;

namespace ClinicManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register Receptionist Repository
            builder.Services.AddScoped<IReceptionistRepository, ReceptionistRepositoryImpl>();
            //Register Receptionist Service
            builder.Services.AddScoped<IReceptionistService, ReceptionistServiceImpl>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Receptionist}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
