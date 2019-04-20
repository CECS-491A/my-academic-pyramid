
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.Models;
using ServiceLayer.UserManagement.UserClaimServices;

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
            if (Contain(user))
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
            if (Contain(user))
            {
                _DbContext.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                return user;
            }
            else
            {
                return null;
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
            if (Contain(user))
            {
                _DbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                return user;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Find user by providing a user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User FindUserByUserEmail(string userEmail)
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
            User user = _DbContext.Set<User>().FirstOrDefault( u => u.UserName.Equals(username));
            return user;
        }

        /// <summary>
        /// Return list of users in database
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUser()
        {
            List<User> list = _DbContext.Set<User>().ToList();
            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// Checks that user is in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Contain(User user)
        {
            List<User> list = GetAllUser();
            if (list.Contains(user))
            {
                return true;
            }
            else
            {
                return false;
            }
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
            Claim existingClaim = _DbContext.Claims.FirstOrDefault(c => c.Value == claim.Value);
            if(existingClaim == null)
            {            
                user.Claims.Add(claim);
            }

            else
            {
                existingClaim.Users.Add(user);
            }

            return user;
        }

        public User AssignCatergory(User user, Catergory catergory)
        {
            Catergory existingCatergory = _DbContext.Catergories.FirstOrDefault(c => c.Value == catergory.Value);
            if (existingCatergory == null)
            {
                catergory.Users.Add(user);
                
            }

            else
            {
                existingCatergory.Users.Add(user);
                user.Catergory = existingCatergory;
            }

            return user;
        }

        public User AutomaticClaimAssigning(User user)
        {
            if(user.Catergory.Value.Equals("Student"))
            {
                //Check if user is over 18 year old
                if (user.DateOfBirth.AddYears(18) <= DateTime.Now)
                {
                    user.Claims.Add(new Claim("Over18"));
                }
                //Messenger Feature's claims
                AddClaim(user,new Claim("CanSendMessage"));
                AddClaim(user,new Claim("CanReceiveMessage"));

                //Discussion Forum's claims
                AddClaim(user, new Claim("CanPostQuestion"));
                AddClaim(user, new Claim("CanReceiveQuestion"));

                //User Management's claims
                AddClaim(user, new Claim("CanCreateOwnStudentAccount"));
                AddClaim(user, new Claim("CanEditOwnAccount"));
                AddClaim(user, new Claim("CanDeleteOwnAccount"));              

            }
            if (user.Catergory.Value.Equals("Admin") || user.Catergory.Value.Equals("SystemAdmin"))
            {
                AddClaim(user, new Claim("CanCreateNewStudentAccount"));
                AddClaim(user, new Claim("CanDeleteStudentAccount"));
                AddClaim(user, new Claim("CanEditStudentAccount"));
                AddClaim(user, new Claim("CanEnableOrDisableStudentAccount"));
                AddClaim(user, new Claim("CanAlterStudentAccountUAC"));

            }

                if (user.Catergory.Value.Equals("SystemAdmin"))
            {
                AddClaim(user, new Claim("EnableOrDisableAdminAccount"));
                AddClaim(user, new Claim("CanDeleteAdminAccount"));
                AddClaim(user, new Claim("CanDeleteOtherUser"));
                AddClaim(user, new Claim("CanAlterAdminAccountUAC"));
            }

            return user;
         
        }

    } // end of class
}
