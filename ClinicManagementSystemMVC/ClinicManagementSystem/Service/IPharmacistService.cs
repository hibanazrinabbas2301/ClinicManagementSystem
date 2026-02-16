using ClinicManagementSystem.ViewModel;

namespace ClinicManagementSystem.Service
{
    public interface IpharmacistService
    {
        //view medcine list
        public IEnumerable<MedicineViewModel> GetAllMedicines();

        //add new medcine 
        int AddMedicine(AddMedicineViewModel model);



    }
}
