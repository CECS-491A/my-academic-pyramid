
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.School;
using ServiceLayer.UserManagement.UserClaimServices;
using static ServiceLayer.ServiceExceptions.UserManagementExceptions;

namespace ServiceLayer.UserManagement.UserAccountServices
{

    /// <summary>21
    /// The class contain User Management's features:
    /// Support create user, delete user, update user, find user by id, find user by username,  get all user
    /// Support add claim, remove claim
    /// </summary>
    public class UserManagementServices : IUserAccountServices, IUserClaimServices, IUserManagementServices
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
        public Account CreateUser(Account user)
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
        public Account DeleteUser(Account user)
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
        public Account UpdateUser(Account user)
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
        /// Find user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account FindById(int id)
        {
            Account user = _DbContext.Users.Where(u => u.Id == id)
                                        .Include(u => u.Category)
                                        .FirstOrDefault();
            return user;
        }
        /// <summary>
        /// Find user by user name
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Account FindByUsername(String username)
        {
            Account user = _DbContext.Set<Account>().Where( u => u.UserName.Equals(username)).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// Return list of users in database
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAllUser()
        {
            List<Account> list = _DbContext.Set<Account>().ToList();
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
        public bool Contain(Account user)
        {
            List<Account> list = GetAllUser();
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
        public Account RemoveClaim(Account user, string claimStr)
        {
            if (claimStr == null)
            {
                return null;
            }
            else if (user == null)
            {
                return null;
            }
            else
            {
                Claim claimToRemove = user.Claims.Where(c => c.Value == claimStr)
                                          .FirstOrDefault();
                if (claimToRemove != null)
                {
                    user.Claims.Remove(claimToRemove);
                }
                else
                {
                    user = null;
                }
                return user;
            }
        }

        /// <summary>
        ///  Add a claim to a user account
        /// </summary>
        /// <param name="user"></param>
        /// <param name="claim"></param>
        public Account AddClaim(Account user, Claim claim)
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

        // Luis
        public Account AssignCategory(Account user, Category category)
        {
            Category existingCategory = _DbContext.Categories.FirstOrDefault(c => c.Value == category.Value);
            if (existingCategory == null)
            {
                user.Category = category;
            }
            else
            {
                existingCategory.Users.Add(user);
                user.Category = existingCategory;
            }

            return user;
        }

        // Luis
        public int? FindIdBySsoId(Guid ssoId)
        {
            int? userId = null;
            try
            {
                userId = _DbContext.Users.Where(s => s.SsoId == ssoId)
                                         .Select(s => s.Id)
                                         .First();
            }
            catch(InvalidOperationException)
            {
                userId = null;
            }

            return userId;
        }

        // Luis
        public Category GetCategory(string categoryValue)
        {
            // It can be null.
            return _DbContext.Categories.Where(c => c.Value == categoryValue)
                                        .FirstOrDefault();
        }

        /// <summary>
        /// Gets the profile information of a user
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public UserProfileDTO GetUserProfile(int accountId)
        {
            UserProfileDTO profile =  _DbContext.Students
                .Include("Courses")
                .Where(s => s.AccountId == accountId)
                .Select(s => new UserProfileDTO
                {
                    FirstName = s.Account.FirstName,
                    MiddleName = s.Account.MiddleName,
                    LastName = s.Account.LastName,
                    SchoolName = s.SchoolDepartment.School.Name,
                    DepartmentName = s.SchoolDepartment.Department.Name,
                    Ranking = s.Account.Exp,
                    AllowTelemetry = s.Account.LogTelemetry,
                    Courses = s.Courses.Select(c => c.Course.Name).ToList()
                })
                .FirstOrDefault();
            return profile;
        }


        public Student FindStudentById(int accountId)
        {
            return _DbContext.Students
                             .Where(s => s.AccountId == accountId)
                             .FirstOrDefault();
        }
        
        Boolean UserExists(int accountId)
        {
            bool userExists = false;
            try
            {
                accountId = _DbContext.Users.Where(s => s.Id == accountId)
                                            .Select(s => s.Id)
                                            .First();
                userExists = true;
            }
            catch (InvalidOperationException)
            {
                userExists = false; ;
            }

            return userExists;
        }

        /// <summary>
        /// Update student account method 
        /// </summary>
        /// <param name="user"></param>
        public Student UpdateStudent(Student user)
        {
            if (user == null)
            {
                return null;
            }
            Student studentToUpdate =  FindStudentById(user.AccountId);
            if (studentToUpdate == null)
            {
                throw new NotAStudentException();
            }
            _DbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
            return user;
        }
    } // end of class
}
