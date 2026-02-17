using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.View_Model
{
    public class DoctorDashboardVM
    {
        public List<Appointment> Appointments { get; set; }

        public Diagnosis DiagnosisForm { get; set; }

        public List<Medicine> Medicines { get; set; }

        public List<LabTest> LabTests { get; set; }
    }
}
