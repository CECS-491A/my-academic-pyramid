using DataAccessLayer;
using DataAccessLayer.Models;
using System;

namespace ServiceLayer.UserManagement.UserAccountServices
{

    /// <summary>
    /// Inteface for UserAccountServices
    /// </summary>
    public interface IUserAccountServices
    {
        Account CreateUser(Account user);
        Account DeleteUser(Account user);
        Account UpdateUser(Account user);
        Account FindById(int id);
    }
}
