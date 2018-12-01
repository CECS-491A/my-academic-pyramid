using System;
using System.Collections.Generic;
using System.Text;
using Authorization.Interfaces;

namespace UserManagement.Interfaces
{
    public interface IUserAccountService<CustomUser> where CustomUser : class , IUser
    {
        void CreateUser(CustomUser user);
        void DeleteUser(CustomUser user);
        void UpdateUser(CustomUser user);
        int FindUserbyUserName(string userName);
    }
}
