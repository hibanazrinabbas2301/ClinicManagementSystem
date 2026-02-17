using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Service
{
    public interface ILabTechnicianService
    {
        IEnumerable<LabTest> SelectAllLabTests();

        void InsertLabTest(LabTest labTest);

        LabTest SelectLabTestById(int testId);

        void UpdateLabTest(LabTest labTest);

        IEnumerable<LabTestPrescription> SelectPendingTests();

        void InsertLabResult(LabResult result);

        void UpdateLabTestStatus(int prescriptionId, string status);

        void CreateLabBill(int resultId, decimal amount);

        IEnumerable<LabResult> SelectPatientLabReports(int patientId);
    }
}
