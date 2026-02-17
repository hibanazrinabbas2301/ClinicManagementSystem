using ClinicManagementSystem.Models;
using ClinicManagementSystem.Repository;

namespace ClinicManagementSystem.Service
{
    public class LabTechnicianService : ILabTechnicianService
    {
        private readonly ILabTechnicianRepository _labTechnicianRepository;

        // DI
        public LabTechnicianService(ILabTechnicianRepository labTechnicianRepository)
        {
            _labTechnicianRepository = labTechnicianRepository;
        }

        #region Lab Test

        public IEnumerable<LabTest> SelectAllLabTests()
        {
            return _labTechnicianRepository.GetAllLabTests();
        }

        public void InsertLabTest(LabTest labTest)
        {
            _labTechnicianRepository.AddLabTest(labTest);
        }

        public LabTest SelectLabTestById(int testId)
        {
            return _labTechnicianRepository.GetLabTestById(testId);
        }

        public void UpdateLabTest(LabTest labTest)
        {
            _labTechnicianRepository.EditLabTest(labTest);
        }

        #endregion


        #region Pending Tests

        public IEnumerable<LabTestPrescription> SelectPendingTests()
        {
            return _labTechnicianRepository.GetPendingTests();
        }

        #endregion


        #region Lab Result

        public void InsertLabResult(LabResult result)
        {
            _labTechnicianRepository.AddLabResult(result);
        }

        public void UpdateLabTestStatus(int prescriptionId, string status)
        {
            _labTechnicianRepository.UpdateLabTestStatus(prescriptionId, status);
        }

        #endregion


        #region Lab Billing

        public void CreateLabBill(int resultId, decimal amount)
        {
            _labTechnicianRepository.GenerateLabBill(resultId, amount);
        }

        #endregion


        #region Patient Reports

        public IEnumerable<LabResult> SelectPatientLabReports(int patientId)
        {
            return _labTechnicianRepository.GetPatientLabReports(patientId);
        }

        #endregion

    }
}
