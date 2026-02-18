using ClinicManagementSystem.Models;
using ClinicManagementSystem.ViewModel;
using System.Collections.Generic;

namespace ClinicManagementSystem.Services
{
    public interface IpharmacistService
    {
        // Medicine
        IEnumerable<MedicineViewModel> GetAllMedicines();
        int AddMedicine(AddMedicineViewModel model);
        IEnumerable<Category> GetCategories();
        int UpdateMedicine(UpdateMedicineViewModel model);

        // Prescription Flow
        IEnumerable<PendingAppointmentViewModel> GetPendingAppointments();
        IEnumerable<PrescriptionDetailViewModel> GetPrescriptionDetails(int appointmentId);
        string IssuePrescription(int appointmentId);


        IEnumerable<IssuedMedicineBillViewModel> GetIssuedMedicinesBill();
    }
}
