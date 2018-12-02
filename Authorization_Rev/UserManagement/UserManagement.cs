using System;
using System.Collections.Generic;
using UserManagement.Interfaces;
using Authorization.Interfaces;

namespace UserManagement
{
    public class UserManagement<CustomUser>:  IUserClaimService<CustomUser>, IUserAccountService<CustomUser> where CustomUser:class, IUser
    {
        List<CustomUser> DbContext;

        public UserManagement()
        {
            DbContext = new List<CustomUser>();
        }
        public void CreateUser(CustomUser user)
        {
            
            DbContext.Add(user);

            if(DbContext.Exists(u => u.UserName.Equals(user.UserName)))
            {
                Console.WriteLine("Add User Succesfully");
            }
            
        }

        public void DeleteUser(CustomUser user)
        {
            DbContext.Remove(user);
        }

        public void UpdateUser(CustomUser user)
        {
            throw new NotImplementedException();
        }

        public int FindUserbyUserName(string userName)
        {
            int userIndex = DbContext.FindIndex( u => u.UserName.Equals(userName));
            return userIndex;
        }

        public void AddClaim(String userName, string claim)
        {

            int userIndex = FindUserbyUserName(userName);
            DbContext[userIndex].userClaims.Add(claim);

            if(DbContext[userIndex].userClaims.Exists(c => c.Equals(claim)))
            {
                Console.WriteLine("Add claim succesfully");
            }
            
        }

        public IList<string> GetClaims(CustomUser user)
        {

            return user.userClaims;
        }

        public void RemoveClaim(CustomUser user, string claim)
        {
            user.userClaims.Remove(claim);
        }

        public void AddClaim(CustomUser user, string claim)
        {
            throw new NotImplementedException();
        }

    }
}
