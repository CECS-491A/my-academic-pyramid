using System.Collections.Generic;
using DataAccessLayer.Models;

namespace ServiceLayer.UserManagement.UserAccountServices
{
    public interface IUserManagementServices
    {
        User AddClaim(User user, Claim claim);
        User AssignCategory(User user, Category category);
        bool Contain(User user);
        User CreateUser(User user);
        User DeleteUser(User user);
        User FindById(int id);
        User FindByUsername(string username);
        User FindUserByUserEmail(string userEmail);
        List<User> GetAllUser();
        User RemoveClaim(User user, string claimStr);
        User UpdateUser(User user);
    }
}