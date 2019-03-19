using DataAccessLayer;
using System;
using System.Collections.Generic;
using SecurityLayer.Authorization.AuthorizationManagers;
using ServiceLayer.UserManagement.UserAccountServices;
using ServiceLayer.PasswordChecking.HashFunctions;
using DataAccessLayer.Models;
using DataAccessLayer.DTOs;
using System.Data.Entity;
using System.Linq;
using ServiceLayer.PasswordChecking.SaltFunction;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.Validation;

namespace ManagerLayer.UserManagement
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
        public User CreateUserAccount(UserDTO userDto)
        {
            try
            {
                var valid = new System.Net.Mail.MailAddress(userDto.Email); // checks that email is valid
            }
            catch (Exception)
            {
                return null;
            }

            SHA256HashFunction HashFunction = new SHA256HashFunction();
            HashSalt hashSaltPassword = HashFunction.GetHashValue(userDto.RawPassword);
            User user = new User
            {
                UserName = userDto.UserName,
                Firstname = userDto.Firstname,
                LastName = userDto.LastName,
                PasswordHash = hashSaltPassword.Hash,
                PasswordSalt = hashSaltPassword.Salt,
                Role = userDto.Role,
                // date and time as it would be in Coordinated Universal Time
                CreatedAt = DateTime.UtcNow, // https://stackoverflow.com/questions/62151/datetime-now-vs-datetime-utcnow 
                DateOfBirth = userDto.BirthDate,
                Location = userDto.Location,
                Email = userDto.Email,
                PasswordQuestion1 = userDto.PasswordQuestion1,
                PasswordQuestion2 = userDto.PasswordQuestion2,
                PasswordQuestion3 = userDto.PasswordQuestion3,
                PasswordAnswer1 = userDto.PasswordAnswer1,
                PasswordAnswer2 = userDto.PasswordAnswer2,
                PasswordAnswer3 = userDto.PasswordAnswer3,
            };

            var response = _userManagementServices.CreateUser(user);
            try
            {
                _DbContext.SaveChanges();
                return user;
            }
            catch (DbEntityValidationException ex)
            {
                // detach user attempted to be created from the db context - rollback
                _DbContext.Entry(response).State = System.Data.Entity.EntityState.Detached;
            }
            return null;
        }

        /// <summary>
        /// Method to delete self user account or other account
        /// </summary>
        /// <param name="targetedUserName"></param>
        public int DeleteUserAccount(User user)
        {
            _userManagementServices.DeleteUser(user);
            return _DbContext.SaveChanges();
        }

        public int UpdateUserAccount(User user)
        {
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

        public User AssignUserToUser(int linkFromUser, int linkToUser )
        {
            User linkFromUser_Searched = FindUser(linkFromUser);
            User linkToUser_Searched = FindUser(linkToUser);

            if (linkFromUser_Searched == null)
            {
                return null;
            }
            else if (linkToUser_Searched == null)
            {
                return null;
            }
            else
            {
                linkFromUser_Searched.ParentUser = linkToUser_Searched;
                return _userManagementServices.UpdateUser(linkFromUser_Searched);
            }
        }

        public User FindUser(String userEmail)
        {
            User user = _userManagementServices.FindUserbyUserEmail(userEmail);
            return user;
        }

        public User FindUser(int id)
        {
            User user = _userManagementServices.FindById(id);
            return user;
        }

        public User FindUserName(string UserName)
        {
            User user = _userManagementServices.FindByUsername(UserName);
            return user;
        }

        /// <summary>
        /// Method to get all users in database 
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUser()
        {
            // Call GetAllUser method in userManagementServices 
            List<User> userList = _userManagementServices.GetAllUser();
            return userList;
        }

        /// <summary>
        /// Method to add claim to user account
        /// The method will take username of the account whom give the claim to and a claim 
        /// </summary>
        /// <param name="targetedUserName"></param>
        /// <param name="claim"></param>
        /// 
        public User AddClaimAction(int targetedUserID, Claim claim)
        {
            // List of required claims needed for AddClaimAction Method
            List<Claim> createUserRequiredClaimTypes = new List<Claim>
            {

                new Claim("UserManager")
            };

            // Check if the requesting user has the require claims
 
                // Retrive targeted user exists from database
            User targetedUser = FindUser(targetedUserID);
            if (targetedUser == null)
            {
                return null;
            }
            // Check if the requesting user is  at least same level as  the targeted user

            else
            {
                _userManagementServices.AddClaim(targetedUser, claim);
                return targetedUser;
            }
        }

        /// <summary>
        /// Method to remove claim from user
        /// </summary>
        /// <param name="targetedUserName"></param>
        /// <param name="claim"></param>
        public User RemoveClaimAction(int targetedUserId, Claim claim)
        {
            // List of required claims needed for AddClaimAction Method
            List<Claim> createUserRequiredClaimTypes = new List<Claim>
            {
                new Claim("UserManager")
            };

            // Check if the requesting user has the require claims

                // Retrive targeted user exists from database
                User targetedUser = FindUser(targetedUserId);
            if (targetedUser == null)
             {
                return null;
               
             }
                // Check if the requesting user is  at least same level as  the targeted user
            else
            {
                _userManagementServices.RemoveClaim(targetedUser, claim);
                return targetedUser;
            }
                   
        }

        public User ChangePassword(int userId, String newPassword)
        {
            User user = FindUser(userId);
            if (user == null)
            {
                return null;
            }
            else
            {
                user.PasswordHash = newPassword;
                _userManagementServices.UpdateUser(user);
                return user;
            }
        }

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


        //public void ChangeSecurityPasswordQuestion(String userName, int questionNumber, String questionContext)
        //{
        //    PasswordQA passwordQA = userManagementServices.FindSecurityQAs(userName);
        //    string choice;
        //    if(questionNumber ==1)
        //    {
        //        choice = "Question1";
        //    }
        //    if (questionNumber == 2)
        //    {
        //        choice = "Question2";
        //    }
        //    if (questionNumber == 3)
        //    {
        //        choice = "Question3";
        //    }

        //    passwordQA.


        //}

    }
}
