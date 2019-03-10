using DataAccessLayer.Models;
using System;
using DataAccessLayer;

namespace ServiceLayer.UserManagement.UserAccountServices
{

    /// <summary>
    /// Inteface for UserAccountServices
    /// </summary>
    public interface IUserAccountServices
    {
        User CreateUser(DatabaseContext _db, User user);
        User DeleteUser(DatabaseContext _db, Guid Id);
        User UpdateUser(DatabaseContext _db, User user);
    }
}
