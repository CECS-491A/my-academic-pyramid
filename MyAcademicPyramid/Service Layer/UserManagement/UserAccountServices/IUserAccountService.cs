using DataAccessLayer;
using DataAccessLayer.Models;

namespace ServiceLayer.UserManagement.UserAccountServices
{

    /// <summary>
    /// Inteface for UserAccountServices
    /// </summary>
    public interface IUserAccountServices
    {
        User CreateUser(User user);
        User DeleteUser(User user);
        User UpdateUser(User user);
        User FindUserbyUserEmail(string userEmail);
        User FindById(int id);
    }
}
