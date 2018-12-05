namespace DataAccessLayer.UserManagement.UserAccountServices
{
    public interface IUserAccountService
    {
        void CreateUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        User FindUserbyUserName(string userName);
    }
}
