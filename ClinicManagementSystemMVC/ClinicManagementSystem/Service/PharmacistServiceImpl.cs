using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repository;
using ClinicManagementSystem.ViewModel;

namespace ClinicManagementSystem.Service
{
    public class PharmacistServiceImpl : IpharmacistService
    {
        private readonly IPharmacistRepo _pharmacistRepo;

        public PharmacistServiceImpl(IPharmacistRepo pharmacistRepo)
        {
            _pharmacistRepo = pharmacistRepo;
        }

        public IEnumerable<MedicineViewModel> GetAllMedicines()
        {
            return _pharmacistRepo.GetAllMedicines();
        }

        public int AddMedicine(AddMedicineViewModel model)
        {
            return _pharmacistRepo.AddMedicine(model);
        }


        public IEnumerable<Category> GetCategories()
        {
            return _pharmacistRepo.GetCategories();
        }


        public int UpdateMedicine(UpdateMedicineViewModel model)
        {
            return _pharmacistRepo.UpdateMedicine(model);
        }


        public IEnumerable<PrescriptionViewModel> GetPendingPrescriptions()
        {
            return _pharmacistRepo.GetPendingPrescriptions();
        }


        public IEnumerable<PrescriptionViewModel> GetPrescriptionDetailsById(int prescriptionId)
        {
            return _pharmacistRepo.GetPrescriptionDetailsById(prescriptionId);
        }



        public int IssuePrescription(int prescriptionId)
        {
            return _pharmacistRepo.IssuePrescription(prescriptionId);
        }



    }

}
