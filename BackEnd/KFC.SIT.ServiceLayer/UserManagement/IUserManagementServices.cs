using System;
using System.Collections.Generic;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;

namespace ServiceLayer.UserManagement.UserAccountServices
{
    public interface IUserManagementServices
    {
        Account AddClaim(Account user, Claim claim);
        Account AssignCategory(Account user, Category category);
        bool Contain(Account user);
        Account CreateUser(Account user);
        Account DeleteUser(Account user);
        Account FindById(int id);
        Account FindByUsername(string username);
        int? FindIdBySsoId(Guid ssoId);
        List<Account> GetAllUser();
        Category GetCategory(string categoryValue);
        Account RemoveClaim(Account user, string claimStr);
        Account UpdateUser(Account user);
        UserProfileDTO GetUserProfile(int accountId);
    }
}