namespace ManagerLayer.Logic.UserManagement.UserManagements
{
    public interface IUserManagement
    {
        void EnableUser();
        void DisableUser();
        void DeleteOtherAccount();
        void DeleteUserPost();
        void DeleteUserOwnAccount();
    }
}