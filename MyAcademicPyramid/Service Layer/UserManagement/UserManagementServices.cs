
using System;
using System.Collections.Generic;
using DataAccessLayer;
using System.Linq;
using ServiceLayer.UserManagement.UserClaimServices;
using System.Data.Entity;

namespace ServiceLayer.UserManagement.UserAccountServices
{

    /// <summary>21
    /// The class contain User Management's features:
    /// Support create user, delete user, update user, find user by id, find user by username,  get all user
    /// Support add claim, remove claim
    /// </summary>
    public class UserManagementServices : IUserAccountServices, IUserClaimServices
    {

        protected DatabaseContext _DbContext;

        /// <summary>
        /// Constructor which initialize the userRepository 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public UserManagementServices(DatabaseContext DbContext)
        {
            _DbContext = DbContext;
        }

        /// <summary>
        /// Create user account method
        /// </summary>
        /// <param name="user"></param>
        public User CreateUser(User user)
        {
            if (user == null)
            {
                return null;
            }
            else
            { 
                _DbContext.Entry(user).State = System.Data.Entity.EntityState.Added;
                return user;
            }
        }

        /// <summary>
        /// Delete user account  
        /// </summary>
        /// <param name="user"></param>
        public User DeleteUser(User user)
        {
            if (user == null)
            {
                return null;
            }
            else
            {
                _DbContext.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                return user;
            }
        }

        /// <summary>
        /// Update user account method 
        /// </summary>
        /// <param name="user"></param>
        public User UpdateUser(User user)
        {
            if (user == null)
            {
                return null;
            }
            else
            {
                _DbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                return user;
            }
        }

        /// <summary>
        /// Find user by providing a user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User FindUserbyUserEmail(string userEmail)
        {
            User user = _DbContext.Set<User>().FirstOrDefault(u => u.Email == userEmail);
            return user;
        }

        /// <summary>
        /// Find user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User FindById(int id)
        {
            User user = _DbContext.Set<User>().Find(id);
            return user;
        }

        public User FindByUsername(String username)
        {
            User user = _DbContext.Set<User>().Find(username);
            return user;
        }

        /// <summary>
        /// Return list of users in database
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUser()
        {
            List<User> list = _DbContext.Set<User>().ToList();
            return list;
        }

        /// <summary>
        /// Remove a claim from a user account
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        public User RemoveClaim(User user, Claim claim)
        {
            if (claim == null)
            {
                return null;
            }
            else if (user == null)
            {
                return null;
            }
            else
            {
                user.Claims.Remove(claim);
                _DbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                return user;
            }
        }

        /// <summary>
        ///  Add a claim to a user account
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        public User AddClaim(User user, Claim claim)
        {
            if (claim == null)
            {
                return null;
            }
            else if (user == null)
            {
                return null;
            }
            else
            {
                user.Claims.Add(claim);
                _DbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                return user;
            }
        }

    } // end of class
}
