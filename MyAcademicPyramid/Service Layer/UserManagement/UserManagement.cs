using DataAccessLayer.UserManagement.UserClaimServices;
using System;
using System.Collections.Generic;
using DataAccessLayer.Repository;
using System.Linq;

namespace DataAccessLayer.UserManagement.UserAccountServices
{
    public class UserManagement: IUserAccountService, IUserClaimService
    {

        protected UnitOfWork unitOfWork;
       
        // Constructor which initialize the userRepository 
        public UserManagement(DatabaseContext context)
        {
            unitOfWork = new UnitOfWork(context);
            
        }

        // Create user account  
        public void CreateUser(User user)
        {

            unitOfWork.UserRepository.Insert(user);
            
        }

        // Delete user account  
        public void DeleteUser(User user)
        {
            unitOfWork.UserRepository.Delete(user);
        }

        // Update user account 
        public void UpdateUser(User user)
        {
            unitOfWork.UserRepository.Update(user);
        }

        // Find user by providing a user name
        public User FindUserbyUserName(string userName)
        {
            IQueryable<User> users = unitOfWork.UserRepository.SearchFor(u => u.UserName.Equals(userName));
            return users.FirstOrDefault();

        }

        // Remove a claim from a user account
        public void RemoveClaim(User user, Claim claim)
        {
            unitOfWork.ClaimRepository.Delete(claim);
        }


        // Add a claim to a user account
        public void AddClaim(User user, Claim claim)
        {

            unitOfWork.ClaimRepository.Insert(claim);
        }
    }
}
