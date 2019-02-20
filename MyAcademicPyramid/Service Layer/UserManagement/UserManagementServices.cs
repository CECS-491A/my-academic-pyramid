
using System;
using System.Collections.Generic;
using DataAccessLayer;
using System.Linq;
using ServiceLayer.UserManagement.UserClaimServices;
using DataAccessLayer.Models;

namespace ServiceLayer.UserManagement.UserAccountServices
{

    /// <summary>
    /// The class contain User Management's features:
    /// Support create user, delete user, update user, find user by id, find user by username,  get all user
    /// Support add claim, remove claim
    /// </summary>
    public class UserManagementServices: IUserAccountServices, IUserClaimServices
    {

        protected IUnitOfWork _unitOfWork;

        
        /// <summary>
        /// Constructor which initialize the userRepository 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public UserManagementServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
            this._unitOfWork = unitOfWork;
        }

        
        /// <summary>
        /// Create user account method
        /// </summary>
        /// <param name="user"></param>
        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            // Check if the username exist. Then add the user
            User searchResult = FindUserbyUserName(user.UserName);
            if (searchResult == null)
            {
                _unitOfWork.UserRepository.Insert(user);
                Console.WriteLine("Create user sucessfully");
            }
            else
            {
                throw new ArgumentException(("Error--Username already exists"));
            }

        }

        
        /// <summary>
        /// Delete user account  
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (FindUserbyUserName(user.UserName) != null)
            {

                _unitOfWork.UserRepository.Delete(user);
            }
            else
            {
                throw new ArgumentException(("Error--Cannot find user to delete"));
            }
        }

        /// <summary>
        /// Update user account method 
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (FindUserbyUserName(user.UserName) != null)
            {
                _unitOfWork.UserRepository.Update(user);
            }
            else
            {
                throw new ArgumentException(("Error--Cannot find user to update"));
            }
        }

        /// <summary>
        /// Find user by providing a user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User FindUserbyUserName(string userName)
        {
            IQueryable<User> users = _unitOfWork.UserRepository.SearchFor(u => u.UserName.Equals(userName));
            return users.FirstOrDefault();

        }

        /// <summary>
        /// Find user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User FindById(int id)
        {
            IQueryable<User> users = _unitOfWork.UserRepository.SearchFor(u => u.Id == id);
            return users.FirstOrDefault();
 
        }

        /// <summary>
        /// Return list of users in database
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUser()
        {
            IEnumerable<User> list = _unitOfWork.UserRepository.GetAll();
            return list.ToList();
        }

        /// <summary>
        /// Remove a claim from a user account
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        public void RemoveClaim(User user, Claim claim)
        {
            User searchedUser = FindUserbyUserName(user.UserName);
            Claim searchedClaim = searchedUser.Claims.FirstOrDefault(c=>c.Value==claim.Value);

            if (searchedUser != null && searchedClaim != null)
            {
                searchedUser.Claims.Remove(searchedClaim);
                _unitOfWork.UserRepository.Update(searchedUser);
            }
            else if (searchedUser == null)
            {
                throw new ArgumentException("Couldn't find user to remove claim");
            }
            else if(searchedClaim == null)
            {
                throw new ArgumentException(("Couldn't find claim to remove "));
            }


        }

        /// <summary>
        ///  Add a claim to a user account
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        public void AddClaim(User user, Claim claim)
        {
            User searchedUser = FindUserbyUserName(user.UserName);

            if (searchedUser != null )
            {
                searchedUser.Claims.Add(claim);
                _unitOfWork.UserRepository.Update(searchedUser);
            }
            else if (searchedUser == null)
            {
                throw new ArgumentException("Couldn't  find user to add claim");
            }

        }

        public PasswordQA FindSecurityQAs(String userName)
        {
            User searchedUser = FindUserbyUserName(userName);
            if (searchedUser != null)
            {
                return searchedUser.passwordQA;
            }
            else
            {
                return null;
            }


            
        }

     }
}
