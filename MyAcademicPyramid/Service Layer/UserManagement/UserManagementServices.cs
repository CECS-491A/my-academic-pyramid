﻿
using System;
using System.Collections.Generic;
using DataAccessLayer;
using System.Linq;
using ServiceLayer.UserManagement.UserClaimServices;

namespace ServiceLayer.UserManagement.UserAccountServices
{

    /// <summary>
    /// The class contain User Management's features:
    /// Support create user, delete user, update user, find user by id, find user by username,  get all user
    /// Support add claim, remove claim
    /// </summary>
    public class UserManagementServices: IUserAccountServices, IUserClaimServices
    {

        protected UnitOfWork unitOfWork;

        
        /// <summary>
        /// Constructor which initialize the userRepository 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public UserManagementServices(UnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }
            this.unitOfWork = unitOfWork;
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
                unitOfWork.UserRepository.Insert(user);
                unitOfWork.Save();
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

                unitOfWork.UserRepository.Delete(user);
                unitOfWork.Save();
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
            if (FindById(user.Id) != null)
            {
                unitOfWork.UserRepository.Update(user);
                unitOfWork.Save();
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
            IQueryable<User> users = unitOfWork.UserRepository.SearchFor(u => u.UserName.Equals(userName));
            return users.FirstOrDefault();

        }

        /// <summary>
        /// Find user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User FindById(int id)
        {
            IQueryable<User> users = unitOfWork.UserRepository.SearchFor(u => u.Id == id);
            return users.FirstOrDefault();
 
        }

        /// <summary>
        /// Return list of users in database
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUser()
        {
            IEnumerable<User> list = unitOfWork.UserRepository.GetAll();
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
            bool searchedClaim = searchedUser.Claims.Contains(claim);

            if (searchedUser != null && searchedClaim != false)
            {
                searchedUser.Claims.Remove(claim);
                unitOfWork.UserRepository.Update(searchedUser);
                unitOfWork.Save();
            }
            else if (searchedUser == null)
            {
                throw new ArgumentException("Couldn't find user to remove claim");
            }
            else if(searchedClaim == false)
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
                unitOfWork.UserRepository.Update(searchedUser);
                unitOfWork.Save();
            }
            else if (searchedUser == null)
            {
                throw new ArgumentException("Couldn't  find user to add claim");
            }

        }

     }
}
