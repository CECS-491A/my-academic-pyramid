
using System;
using System.Collections.Generic;
using DataAccessLayer;
using System.Linq;
using ServiceLayer.UserManagement.UserClaimServices;
using System.Data.Entity;

namespace ServiceLayer.UserManagement.UserAccountServices
{

    /// <summary>
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
            if (DbContext == null)
            {
                throw new ArgumentNullException("DbContext");
            }
            else
            {
                _DbContext = DbContext;
            }

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
            else
            { 
                 _DbContext.Entry(user).State = System.Data.Entity.EntityState.Added;
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

                _DbContext.Entry(user).State = System.Data.Entity.EntityState.Deleted;
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
            else
            {
                _DbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
            }

        }

        /// <summary>
        /// Find user by providing a user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User FindUserbyUserName(string userName)
        {
            User user = _DbContext.Set<User>().FirstOrDefault(u => u.UserName == userName);
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
        public void RemoveClaim(User user, Claim claim)
        {

            if (user != null && claim != null)
            {
                user.Claims.Remove(claim);
                _DbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
            }
            else if (user == null)
            {
                throw new ArgumentException("User or claim object is empty");
            }



        }

        /// <summary>
        ///  Add a claim to a user account
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        public void AddClaim(User user, Claim claim)
        {

            if (user != null && claim != null)
            {
                user.Claims.Add(claim);
                _DbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
            }
            else if (user == null)
            {
                throw new ArgumentException("Couldn't  find user to add claim");
            }

        }

    }
}
