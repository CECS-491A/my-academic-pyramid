
using System;
using System.Collections.Generic;
using DataAccessLayer;
using System.Linq;
using ServiceLayer.UserManagement.UserClaimServices;
using System.Data.Entity;
using DataAccessLayer.Repository;
using DataAccessLayer.Models;

namespace ServiceLayer.UserManagement.UserAccountServices
{

    /// <summary>
    /// The class contain User Management's features:
    /// Support create user, delete user, update user, find user by id, find user by username,  get all user
    /// Support add claim, remove claim
    /// </summary>
    public class UserManagementServices : IUserAccountServices, IUserClaimServices
    {
        private UserManagementRepository _UserManagementRepo;

        public UserManagementServices()
        {
            _UserManagementRepo = new UserManagementRepository();
        }

        public User CreateUser(DatabaseContext _db, User user)
        {
            if (_UserManagementRepo.ExistingUser(_db, user))
            {
                Console.WriteLine("User exists");
                return null;
            }
            return _UserManagementRepo.CreateNewUser(_db, user);
        }

        public User DeleteUser(DatabaseContext _db, Guid Id)
        {
            return _UserManagementRepo.DeleteUser(_db, Id);
        }

        public User GetUser(DatabaseContext _db, string email)
        {
            return _UserManagementRepo.GetUser(_db, email);
        }

        public User GetUser(DatabaseContext _db, Guid Id)
        {
            return _UserManagementRepo.GetUser(_db, Id);
        }

        public User UpdateUser(DatabaseContext _db, User user)
        {
            return _UserManagementRepo.UpdateUser(_db, user);
        }

        public User Login(DatabaseContext _db, string email, string password)
        {
            UserRepository userRepo = new UserRepository();
            PasswordService _passwordService = new PasswordService();
            var user = _UserManagementRepo.GetUser(_db, email);
            if (user != null)
            {
                string hashedPassword = _passwordService.HashPassword(password, user.PasswordSalt);
                if (userRepo.ValidatePassword(user, hashedPassword))
                {
                    Console.WriteLine("Password Correct");
                    return user;
                }
                Console.WriteLine("Password Incorrect");
                return null;
            }
            Console.WriteLine("User does not exist");
            return null;
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
                _DbContext.Entry(searchedUser).State = System.Data.Entity.EntityState.Modified;
            }
            else if (searchedUser == null)
            {
                throw new ArgumentException("Couldn't find user to remove claim");
            }
            else if (searchedClaim == false)
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

            if (searchedUser != null)
            {
                searchedUser.Claims.Add(claim);
                _DbContext.Entry(searchedUser).State = System.Data.Entity.EntityState.Modified;
            }
            else if (searchedUser == null)
            {
                throw new ArgumentException("Couldn't  find user to add claim");
            }

        }

    }
}
