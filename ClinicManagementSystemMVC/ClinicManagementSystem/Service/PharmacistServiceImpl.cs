using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repository;
using ClinicManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystem.Service
{
    public class PharmacistServiceImpl : IpharmacistService
    {
        private readonly IPharmacistRepo _pharmacistRepo;

        public PharmacistServiceImpl(IPharmacistRepo pharmacistRepo)
        {
            _pharmacistRepo = pharmacistRepo;
        }

        // Medicine
        public IEnumerable<MedicineViewModel> GetAllMedicines()
            => _pharmacistRepo.GetAllMedicines();

        public int AddMedicine(AddMedicineViewModel model)
            => _pharmacistRepo.AddMedicine(model);

        public int UpdateMedicine(UpdateMedicineViewModel model)
            => _pharmacistRepo.UpdateMedicine(model);

        public IEnumerable<Category> GetCategories()
            => _pharmacistRepo.GetCategories();

        // Prescription Flow
        public IEnumerable<PendingAppointmentViewModel> GetPendingAppointments()
            => _pharmacistRepo.GetPendingAppointments();

        public IEnumerable<PrescriptionDetailViewModel> GetPrescriptionDetails(int appointmentId)
            => _pharmacistRepo.GetPrescriptionDetails(appointmentId);

        public string IssuePrescription(int appointmentId)
             => _pharmacistRepo.IssuePrescription(appointmentId);


        public IEnumerable<IssuedMedicineBillViewModel> GetIssuedMedicinesBill()
             => _pharmacistRepo.GetIssuedMedicinesBill();
    }


}
