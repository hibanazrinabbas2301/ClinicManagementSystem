using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Repositories
{
    public interface ILabTechnicianRepository
    {
        // Lab Test
        IEnumerable<LabTest> GetAllLabTests();
        void AddLabTest(LabTest labTest);
        LabTest GetLabTestById(int testId);
        void EditLabTest(LabTest labTest);

        // Pending Tests
        IEnumerable<LabTestPrescription> GetPendingTests();

        // Lab Result
        int AddLabResult(LabResult result);
        void UpdateLabTestStatus(int prescriptionId, string status);
        LabResult GetResultById(int resultId);

        // Billing
        void GenerateLabBill(int resultId, decimal amount);

        // Patient Reports
        IEnumerable<LabResult> GetPatientLabReports(int patientId);

        IEnumerable<LabTestPrescription> GetCompletedTests();
    }
}
