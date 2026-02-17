using ClinicManagementSystem.View_Model;

namespace ClinicManagementSystem.Services
{
    public interface IUserService
    {
        UserRequired AuthenticateLogin(string username, string password);
    }
}
