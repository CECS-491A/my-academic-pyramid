using ServiceLayer.UserManagement.UserClaimServices;
using System;
using System.Collections.Generic;

namespace ServiceLayer.UserManagement.UserAccountServices
{
    public class UserManagement: IUserAccountService, IUserClaimService
    { 
        List<User> DbContext;

        public UserManagement()
        {
            DbContext = new List<User>();
        }
        public void CreateUser(User user)
        {
            
            DbContext.Add(user);

            if(DbContext.Exists(u => u.UserName.Equals(user.UserName)))
            {
                Console.WriteLine("Add User Succesfully");
            }
            
        }

        public void DeleteUser(User user)
        {
            DbContext.Remove(user);
        }

        public void UpdateUser(User user)
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

        public IList<string> GetClaims(User user)
        {

            return user.userClaims;
        }

        public void RemoveClaim(User user, string claim)
        {
            user.userClaims.Remove(claim);
        }

        public void AddClaim(User user, string claim)
        {
            throw new NotImplementedException();
        }

    }
}
