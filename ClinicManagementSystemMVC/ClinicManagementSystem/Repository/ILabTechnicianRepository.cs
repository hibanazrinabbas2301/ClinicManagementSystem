using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repository
{
    public interface ILabTechnicianRepository
    {
        IEnumerable<LabTest> GetAllLabTests();
        void AddLabTest(LabTest labTest);
        LabTest GetLabTestById(int testId);
        void EditLabTest(LabTest labTest);

        IEnumerable<LabTestPrescription> GetPendingTests();

        void AddLabResult(LabResult result);
        void UpdateLabTestStatus(int prescriptionId, string status);

        void GenerateLabBill(int resultId, decimal amount);

        IEnumerable<LabResult> GetPatientLabReports(int patientId);
    }
}
