
using System;
using System.Collections.Generic;
using DataAccessLayer;
using System.Linq;
using ServiceLayer.UserManagement.UserClaimServices;

namespace ServiceLayer.UserManagement.UserAccountServices
{
    public class UserManagement: IUserAccountService, IUserClaimService
    {

        protected UnitOfWork unitOfWork;
       
        // Constructor which initialize the userRepository 
        public UserManagement()
        {
            unitOfWork = new UnitOfWork();
        }

        // Create user account  
        public void CreateUser(User user)
        {
            // Check if the username exist. Then add the user

                unitOfWork.UserRepository.Insert(user);
                unitOfWork.Save();
            

                

        }

        // Delete user account  
        public void DeleteUser(User user)
        {
            unitOfWork.UserRepository.Delete(user);
            unitOfWork.Save();
        }

        // Update user account 
        public void UpdateUser(User user)
        {
            unitOfWork.UserRepository.Update(user);
            unitOfWork.Save();
        }

        // Find user by providing a user name
        public User FindUserbyUserName(string userName)
        {
            IQueryable<User> users = unitOfWork.UserRepository.SearchFor(u => u.UserName.Equals(userName));
            return users.FirstOrDefault();


        }

        // Find user by Id
        public User FindById(int id)
        {
            IQueryable<User> users = unitOfWork.UserRepository.SearchFor(u => u.Id == id);
            return users.FirstOrDefault();
        }

        // Remove a claim from a user account
        public void RemoveClaim(User user, Claim claim)
        {
            User searchuser = unitOfWork.UserRepository.SearchFor(u => u.Id == user.Id).FirstOrDefault();
            searchuser.Claims.Remove(claim);
            unitOfWork.Save();
        }




        // Add a claim to a user account
        public void AddClaim(User user, Claim claim)
        {
  
                User searchuser = unitOfWork.UserRepository.SearchFor(u => u.Id == user.Id).FirstOrDefault();
                searchuser.Claims.Add(claim);
                unitOfWork.Save();
            }

       

        
    }
}
