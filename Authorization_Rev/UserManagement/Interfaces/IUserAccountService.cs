using Authorization;
using Authorization.Interfaces;

namespace UserManagement.Interfaces
{
    public interface IUserAccountService
    {
        void CreateUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        int FindUserbyUserName(string userName);
    }
}
