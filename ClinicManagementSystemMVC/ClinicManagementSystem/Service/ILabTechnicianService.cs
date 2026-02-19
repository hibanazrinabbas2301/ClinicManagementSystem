using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Services
{
    public interface ILabTechnicianService
    {
        // Lab Test
        IEnumerable<LabTest> SelectAllLabTests();
        void InsertLabTest(LabTest labTest);
        LabTest SelectLabTestById(int testId);
        void UpdateLabTest(LabTest labTest);

        // Pending Tests
        IEnumerable<LabTestPrescription> SelectPendingTests();

        // Lab Result
        int InsertLabResult(LabResult result);
        void UpdateLabTestStatus(int prescriptionId, string status);
        LabResult GetResultById(int resultId);

        // Billing
        void CreateLabBill(int resultId, decimal amount);

        // Patient Reports
        IEnumerable<LabResult> SelectPatientLabReports(int patientId);

        // Completed Tests
        IEnumerable<LabTestPrescription> SelectCompletedTests();

    }
}
