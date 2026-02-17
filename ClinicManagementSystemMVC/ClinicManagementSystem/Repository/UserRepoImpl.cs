using ClinicManagementSystem.Models;
using ClinicManagementSystem.View_Model;
using Microsoft.Data.SqlClient;

namespace ClinicManagementSystem.Repositories
{
    public class UserRepoImpl : IUserRepo
    {
        private readonly string _ConnectionString;

        public UserRepoImpl(IConfiguration configuration)
        {
            _ConnectionString = configuration.GetConnectionString("ConnStringMVC");
        }
        public UserRequired AuthenticateUser(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                var query = @"
                SELECT s.StaffId, s.Name, r.RoleName
                FROM Staff s
                INNER JOIN Roles r
                    ON s.RoleId = r.RoleId
                WHERE s.Username = @Username
                  AND s.Password = @Password
                  AND s.Status = 'Active'
                ";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new UserRequired()
                    {
                        StaffId = Convert.ToInt32(reader["StaffId"]),
                        Name = reader["Name"].ToString(),
                        RoleName = reader["RoleName"].ToString()
                    };
                }
            }

            return null;
        }
    }
}