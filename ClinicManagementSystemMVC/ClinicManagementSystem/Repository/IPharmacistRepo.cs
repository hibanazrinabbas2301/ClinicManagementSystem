using ClinicManagementSystem.Models;
using ClinicManagementSystem.ViewModel;

namespace ClinicManagementSystem.Repository
{
    public interface IPharmacistRepo
    {

        //to list all medicines
        public IEnumerable<MedicineViewModel> GetAllMedicines();

        //add new medcine  to  the medcine 
        int AddMedicine(AddMedicineViewModel model);


        //to get the category of medcine 
        IEnumerable<Category> GetCategories();

        //to edit  the medcine stock 
        public int UpdateMedicine(UpdateMedicineViewModel model);

        public IEnumerable<PrescriptionViewModel> GetPendingPrescriptions();




        IEnumerable<PrescriptionViewModel> GetPrescriptionDetailsById(int prescriptionId);


        int IssuePrescription(int prescriptionId);


    }




}

