namespace ClinicManagementSystem.View_Model
{
    public class DoctorLabResultViewModel
    {
        public string TestName { get; set; }
        public string Status { get; set; }
        public string ActualValue { get; set; }
        public string NormalRange { get; set; }
        public string Remarks { get; set; }
        public DateTime? ResultDate { get; set; }
    }
}