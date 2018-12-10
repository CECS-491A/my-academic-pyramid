
using System;
using System.Collections.Generic;
using DataAccessLayer;
using System.Linq;
using ServiceLayer.UserManagement.UserClaimServices;

namespace ServiceLayer.UserManagement.UserAccountServices
{

    public class UserManagementServices: IUserAccountServices, IUserClaimServices
    {

        protected UnitOfWork unitOfWork;
       
        // Constructor which initialize the userRepository 
        public UserManagementServices(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Create user account  
        public void CreateUser(User user)
        {
            // Check if the username exist. Then add the user
            User searchResult = unitOfWork.UserRepository.SearchFor(u => u.UserName.Equals(user.UserName)).FirstOrDefault();
            if (searchResult == null)
            {
                unitOfWork.UserRepository.Insert(user);
                unitOfWork.Save();
            }
            else
            {
                throw new ArgumentException("User with the username exists");
            }

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

        public List<User> GetAllUser()
        {
            IEnumerable<User> list = unitOfWork.UserRepository.GetAll();
            return list.ToList();
        }



        // Remove a claim from a user account
        public void RemoveClaim(User user, Claim claim)
        {
            User searchuser = unitOfWork.UserRepository.SearchFor(u => u.Id == user.Id).FirstOrDefault();
            searchuser.Claims.Remove(claim);
            unitOfWork.UserRepository.Update(searchuser);
            unitOfWork.Save();

        }


        // Add a claim to a user account
        public void AddClaim(User user, Claim claim)
        {

            User searchuser = unitOfWork.UserRepository.SearchFor(u => u.Id == user.Id).FirstOrDefault();
            searchuser.Claims.Add(claim);
            unitOfWork.UserRepository.Update(searchuser);
                 unitOfWork.Save();
                
            }

        }
}
