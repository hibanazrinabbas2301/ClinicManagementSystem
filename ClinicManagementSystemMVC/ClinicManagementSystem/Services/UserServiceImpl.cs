using ClinicManagementSystem.Repositories;
using ClinicManagementSystem.View_Model;

namespace ClinicManagementSystem.Services
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepo _repo;

        public UserServiceImpl(IUserRepo repo)
        {
            _repo = repo;
        }

        public UserRequired AuthenticateLogin(string username, string password)
        {
            return _repo.AuthenticateUser(username, password);
        }
    }
}

