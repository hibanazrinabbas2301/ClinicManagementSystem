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

    }

}
