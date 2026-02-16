using ClinicManagementSystem.ViewModel;

namespace ClinicManagementSystem.Repository
{
    public interface IPharmacistRepo
    {

        //to list all medicines
        public IEnumerable<MedicineViewModel> GetAllMedicines();

        //add new medcine  to  the medcine 
        int AddMedicine(AddMedicineViewModel model);



    }
}
