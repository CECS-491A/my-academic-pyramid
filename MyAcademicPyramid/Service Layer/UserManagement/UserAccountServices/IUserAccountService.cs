using DataAccessLayer;

namespace ServiceLayer.UserManagement.UserAccountServices
{

    /// <summary>
    /// Inteface for UserAccountServices
    /// </summary>
    public interface IUserAccountServices
    {
        void CreateUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        User FindUserbyUserName(string userName);
        User FindById(int id);
    }
}
