using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Service
{
    public interface IEmailService
    {
        void SendBill(string toEmail, BillViewModel bill);
    }
}
