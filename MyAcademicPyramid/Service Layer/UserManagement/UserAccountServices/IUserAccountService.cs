using DataAccessLayer;

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
        User FindUserbyUserName(string userName);
        User FindById(int id);
    }
}
