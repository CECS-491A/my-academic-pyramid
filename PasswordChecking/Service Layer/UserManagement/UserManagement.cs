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
       

        public UserManagement()
        {
            _userRepository = new UserRepository();
            
        }
        public void CreateUser(User user)
        {

            _userRepository.Insert(user);
            
        }

        public void DeleteUser(User user)
        {
            _userRepository.Delete(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        public User FindUserbyUserName(string userName)
        {
            IQueryable<User> users = _userRepository.SearchFor(u => u.UserName.Equals(userName));
            return users.FirstOrDefault();
        }


        public IList<string> GetClaims(User user)
        {

            return user.Claims;
        }

        public void RemoveClaim(User user, string claim)
        {
            user.Claims.Remove(claim);
        }

        public void AddClaim(User user, string claim)
        {
            
            user.Claims.Add(claim);
        }
    }
}
