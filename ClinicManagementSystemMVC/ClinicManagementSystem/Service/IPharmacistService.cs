using ClinicManagementSystem.Models;
using ClinicManagementSystem.ViewModel;
using System.Collections.Generic;

namespace ClinicManagementSystem.Services
{
    public interface IpharmacistService
    {
        //view medcine list
        public IEnumerable<MedicineViewModel> GetAllMedicines();

        //add new medcine 
        int AddMedicine(AddMedicineViewModel model);

        //category 
        IEnumerable<Category> GetCategories();

        //edit medcine stock

        public int UpdateMedicine(UpdateMedicineViewModel model);


        public IEnumerable<PrescriptionViewModel> GetPendingPrescriptions();


        IEnumerable<PrescriptionViewModel> GetPrescriptionDetailsById(int prescriptionId);



        int IssuePrescription(int prescriptionId);
















    }
}
