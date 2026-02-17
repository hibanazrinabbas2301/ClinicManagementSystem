using ClinicManagementSystem.View_Model;

namespace ClinicManagementSystem.Repositories
{
    public interface IUserRepo
    {
        UserRequired AuthenticateUser(string username, string password);
    }
}
