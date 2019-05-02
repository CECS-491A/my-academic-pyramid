using DataAccessLayer;
using System;
using System.Collections.Generic;
using ServiceLayer.UserManagement.UserAccountServices;
using ServiceLayer.PasswordChecking.HashFunctions;
using DataAccessLayer.Models;
using DataAccessLayer.DTOs;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.Validation;

namespace WebAPI.UserManagement
{
    public class UserManager
    {
        /*
         * Class that demonstrates how authorization class might be used in
         * a controller.
         * */
        private UserManagementServices _userManagementServices;
        protected DatabaseContext _DbContext;

        /// <summary>
        /// Constructor that accept a username of account and initiaize the UserManagementServices
        /// User Account has to exist in database
        /// </summary>
        /// <param name="requestingUserName"></param>
        public UserManager()
        {
            _DbContext = new DatabaseContext();
            _userManagementServices = new UserManagementServices(_DbContext);
        }

        /// <summary>
        /// Method to create user account
        /// </summary>
        /// <param name="targetedUserName"></param>
        public Account CreateUserAccount(UserDTO userDto)
        {
            Category category = _userManagementServices.GetCategory(userDto.Category);
            if (category == null)
            {
                category = new Category(userDto.Category);
            }
            Account user = new Account
            {
                UserName = userDto.UserName,
                SsoId = userDto.SsoId,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Category = category
            };

            //Automatically assigning claim to user
            user = AutomaticClaimAssigning(user);
            // TODO test this.
            //user = _userManagementServices.AssignCategory(user, new Category(userDto.Category));

            var response = _userManagementServices.CreateUser(user);
            try
            {
                _DbContext.SaveChanges();
                return response;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

        }

        /// <summary>
        /// Method to delete self user account or other account
        /// </summary>
        /// <param name="targetedUserName"></param>
        public int DeleteUserAccount(Account user)
        {
            if (user == null)
            {
                return 0;
            }
            _userManagementServices.DeleteUser(user);
            return _DbContext.SaveChanges();
        }

        /// <summary>
        /// Method to update user account in database after making changes
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int UpdateUserAccount(Account user)
        {
            if (user == null)
            {
                return 0;
            }
            var response = _userManagementServices.UpdateUser(user);
            try
            {
                return _DbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // catch error
                // rollback changes
                _DbContext.Entry(response).CurrentValues.SetValues(_DbContext.Entry(response).OriginalValues);
                _DbContext.Entry(response).State = System.Data.Entity.EntityState.Unchanged;
                return 0;
            }
        }

        /// <summary>
        /// Method to assign a user to be a child of another user. The purpose is to achive a hierachy structure of users
        /// </summary>
        /// <param name="childUserName"></param>
        /// <param name="parentUserName"></param>
        /// <returns></returns>
        public Account AssignUserToUser(string childUserName, string parentUserName )
        {
            Account childUser = FindByUserName(childUserName);
            Account parentUser = FindByUserName(parentUserName);

            if (childUser == null)
            {
                return null;
            }
            else if (parentUser == null)
            {
                return null;
            }
            else
            {
                if (childUser.Id == parentUser.ParentUser_Id)
                {
                    parentUser.ParentUser_Id = null;
                }
                childUser.ParentUser_Id = parentUser.Id;

                UpdateUserAccount(childUser);
                UpdateUserAccount(parentUser);

                return childUser;
            }
        }

        /// <summary>
        /// Method to find user object using email
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public Account FindUserByEmail(String userEmail)
        {
            Account user = _userManagementServices.FindByUsername(userEmail);
            return user;
        }

        /// <summary>
        /// Method to find userobject using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account FindUserById(int id)
        {
            Account user = _userManagementServices.FindById(id);
            return user;
        }

        public UserDTO GetUserInfo(int id)
        {
            Account user = FindUserById(id);
            UserDTO userDTO = null;
            if (user != null)
            {
                userDTO = new UserDTO()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Category = user.Category.Value,
                    DateOfBirth = user.DateOfBirth.ToLongDateString(),
                    CreatedAt = user.CreatedAt.ToLongDateString()
                };
            }
            return userDTO;
        }

        /// <summary>
        /// Method to find user object by UserName
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public Account FindByUserName(string UserName)
        {
            Account user = _userManagementServices.FindByUsername(UserName);
            return user;
        }

        /// <summary>
        /// Method to get all users in database
        /// </summary>
        /// <returns></returns>
        public List<Account> GetAllUser()
        {
            // Call GetAllUser method in userManagementServices
            List<Account> userList = _userManagementServices.GetAllUser();
            return userList;
        }

        /// <summary>
        /// Method to remove claim from user
        /// </summary>
        /// <param name="targetedUserName"></param>
        /// <param name="claim"></param>
        public Account RemoveClaimAction(int targetedUserId, string claimStr)
        {
            // List of required claims needed for AddClaimAction Method
            List<Claim> createUserRequiredClaimTypes = new List<Claim>
            {
                new Claim("UserManager")
            };

            // Check if the requesting user has the require claims

                // Retrive targeted user exists from database
                Account targetedUser = FindUserById(targetedUserId);
            if (targetedUser == null)
             {
                return null;

             }
                // Check if the requesting user is  at least same level as  the targeted user
            else
            {
                Account user = _userManagementServices.RemoveClaim(targetedUser, claimStr);
                UpdateUserAccount(user);
                return user;
            }

        }

        public Account SetCategory(int targetUserId, string categoryStr)
        {
            Category categoryToAdd = _userManagementServices.GetCategory(categoryStr);
            if (categoryToAdd == null)
            {
                categoryToAdd = new Category(categoryStr);
            }
            Account targetUser = _userManagementServices.FindById(targetUserId);
            if (targetUser == null)
            {
                return null;
            }
            _userManagementServices.AssignCategory(targetUser, categoryToAdd);

            _DbContext.SaveChanges();
            return targetUser;
        }

        public Account AddClaimAction(int targetedUserId, Claim claim)
        {
            // TODO finish this. It assigns a list instead of adding a claim.
            // List of required claims needed for AddClaimAction Method

            // Check if the requesting user has the require claims

            // Retrive targeted user exists from database
            Account targetedUser = FindUserById(targetedUserId);
            if (targetedUser == null)
            {
                return null;

            }
            // Check if the requesting user is  at least same level as  the targeted user
            else
            {
                Account user = _userManagementServices.AddClaim(targetedUser, claim);
                UpdateUserAccount(user);
                return user;
            }

        }

        public List<string> GetClaims(string username)
        {
            Account user = _userManagementServices.FindByUsername(username);
            List<string> claimList = new List<string>();
            foreach(var claim in user.Claims)
            {
                claimList.Add(claim.Value);
            }
            return claimList;
        }

        //
        /// <summary>
        /// Method to verify password when user login. Entered password will be hashed using input from user plus the salt value
        /// The hashed password then will be compared with the value in database for validation.
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(enteredPassword + storedSalt);

            SHA256 sha = new SHA256Cng(); // Hash function
            byte[] hash = sha.ComputeHash(bytes); // Generate hash in bytes

            // Store the hash value as string with uppercase letters.
            StringBuilder hashPassword = new StringBuilder(); // To store the hash value
            foreach (byte b in hash)
            {
                hashPassword.Append(b.ToString("X2"));
            }
            String hashSaltPassword = hashPassword.ToString();

            return (hashSaltPassword.Equals(storedHash));

        }

        public Account AutomaticClaimAssigning(Account user)
        {
            if (user.Category.Value.Equals("Student"))
            {
                //Check if user is over 18 year old
                if (user.DateOfBirth.AddYears(18) <= DateTime.Now)
                {
                    user.Claims.Add(new Claim("Over18"));
                }
                //Messenger Feature's claims
                _userManagementServices.AddClaim(user, new Claim("CanSendMessage"));
                _userManagementServices.AddClaim(user, new Claim("CanReceiveMessage"));

                //Discussion Forum's claims
                _userManagementServices.AddClaim(user, new Claim("CanPostQuestion"));
                _userManagementServices.AddClaim(user, new Claim("CanReceiveQuestion"));

                //User Management's claims
                _userManagementServices.AddClaim(user, new Claim("CanCreateOwnStudentAccount"));
                _userManagementServices.AddClaim(user, new Claim("CanEditOwnAccount"));
                _userManagementServices.AddClaim(user, new Claim("CanDeleteOwnAccount"));
                _userManagementServices.AddClaim(user, new Claim("CanReadOwnStudentAccount"));
                _DbContext.SaveChanges();
            }
            else if (user.Category.Value.Equals("Admin") || user.Category.Value.Equals("SystemAdmin"))
            {
                _userManagementServices.AddClaim(user, new Claim("CanCreateNewStudentAccount"));
                _userManagementServices.AddClaim(user, new Claim("CanDeleteStudentAccount"));
                _userManagementServices.AddClaim(user, new Claim("CanEditStudentAccount"));
                _userManagementServices.AddClaim(user, new Claim("CanEnableOrDisableStudentAccount"));
                _userManagementServices.AddClaim(user, new Claim("CanAlterStudentAccountUAC"));
                _DbContext.SaveChanges();
            }

            else if (user.Category.Value.Equals("SystemAdmin"))
            {
                _userManagementServices.AddClaim(user, new Claim("EnableOrDisableAdminAccount"));
                _userManagementServices.AddClaim(user, new Claim("CanDeleteAdminAccount"));
                _userManagementServices.AddClaim(user, new Claim("CanDeleteOtherUser"));
                _userManagementServices.AddClaim(user, new Claim("CanAlterAdminAccountUAC"));
                _DbContext.SaveChanges();
            }

            return user;

        }

    }
}
