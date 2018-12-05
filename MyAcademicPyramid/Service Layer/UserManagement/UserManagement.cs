using DataAccessLayer.UserManagement.UserClaimServices;
using System;
using System.Collections.Generic;
using DataAccessLayer.Repository;
using System.Linq;

namespace DataAccessLayer.UserManagement.UserAccountServices
{
    public class UserManagement: IUserAccountService, IUserClaimService
    {
     
        private readonly UserRepository _userRepository;
       
        // Constructor which initialize the userRepository 
        public UserManagement()
        {
            _userRepository = new UserRepository();
            
        }

        // Create user account  
        public void CreateUser(User user)
        {

            _userRepository.Insert(user);
            
        }

        // Delete user account  
        public void DeleteUser(User user)
        {
            _userRepository.Delete(user);
        }

        // Update user account 
        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        // Find user by providing a user name
        public User FindUserbyUserName(string userName)
        {
            IQueryable<User> users = _userRepository.SearchFor(u => u.UserName.Equals(userName));
            return users.FirstOrDefault();
        }

        // Get claim method from a user account
        public IList<string> GetClaims(User user)
        {

            return user.Claims;
        }

        // Remove a claim from a user account
        public void RemoveClaim(User user, string claim)
        {
            user.Claims.Remove(claim);
        }


        // Add a claim to a user account
        public void AddClaim(User user, string claim)
        {
            
            user.Claims.Add(claim);
        }
    }
}
