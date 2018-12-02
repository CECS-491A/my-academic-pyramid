namespace ServiceLayer.UserManagement.UserAccountServices
{
    public interface IUserAccountService
    {
        void CreateUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        int FindUserbyUserName(string userName);
    }
}
