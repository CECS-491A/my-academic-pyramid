using DataAccessLayer;
using System;
using System.Collections.Generic;
using SecurityLayer.Authorization.AuthorizationManagers;
using ServiceLayer.UserManagement.UserAccountServices;
using ServiceLayer.PasswordChecking.HashFunctions;
using System.Data.Entity;
using System.Linq;

namespace ManagerLayer.UserManagement
{
    public class UserManager
    {
        /*
         * Class that demonstrates how authorization class might be used in
         * a controller.
         * */
        private UserManagementServices userManagementServices;
        protected DatabaseContext _DbContext;


        /// <summary>
        /// Constructor that accept a username of account and initiaize the UserManagementServices
        /// User Account has to exist in database 
        /// </summary>
        /// <param name="requestingUserName"></param>
        public UserManager(DatabaseContext DbContext)
        {
            _DbContext = DbContext;
            // Call UserManagementServices 
            userManagementServices = new UserManagementServices(_DbContext);
    
        }


        /// <summary>
        /// Method to delete self user account or other account
        /// </summary>
        /// <param name="targetedUserName"></param>
        public void DeleteUserAction(String targetedUserName)
        {
            if (targetedUserName == null)
            {
                throw new ArgumentNullException("targetedUserName");
            }
            User targetUser = userManagementServices.FindUserbyUserName(targetedUserName);
            if (targetUser == null)
            {
                throw new ArgumentNullException(
                    "_targetedUser", "The user to be deleted doesn't exist.");
            }
            else
            {
                userManagementServices.DeleteUser(targetUser);
            }

        }

        /// <summary>
        /// Method to create user account
        /// </summary>
        /// <param name="targetedUserName"></param>
        public void CreateUserAction(User user, String hashedPassword)
        {
            if (user == null)
            {
                throw new ArgumentNullException("targetedUserName");
            }
            // Call AuthorizationManager and pass the requesting user object in
            if(userManagementServices.FindUserbyUserName(user.UserName) == null)
            {

                // Tell userManagement services to create user in database 
                user.HashPassword = hashedPassword;
                userManagementServices.CreateUser(user);
                
            }
            else
            {
                throw new ArgumentException(
                    "ERROR--Username is already used"
                    );
               
            }
        }

        public void AssignUserToUser(User childUser, User parentUser )
        {
            childUser = userManagementServices.FindUserbyUserName(childUser.UserName);
            parentUser = userManagementServices.FindUserbyUserName(parentUser.UserName);
            childUser.ParentUser = parentUser;
            userManagementServices.UpdateUser(childUser);

        }


        /// <summary>
        /// Method to get all users in database 
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUser()
        {
            // Call GetAllUser method in userManagementServices 
            List<User> userList = userManagementServices.GetAllUser();
            return userList;
        }

        /// <summary>
        /// Method to add claim to user account
        /// The method will take username of the account whom give the claim to and a claim 
        /// </summary>
        /// <param name="targetedUserName"></param>
        /// <param name="claim"></param>
        /// 
        public void AddClaimAction(string targetedUserName, Claim claim)
        {
            if (targetedUserName == null)
            {
                throw new ArgumentNullException("targetedUserName");
            }
            else if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            // Call AuthorizationManager and pass the requesting user object in

            // List of required claims needed for AddClaimAction Method
            List<Claim> createUserRequiredClaimTypes = new List<Claim>
            {

                new Claim("UserManager")
            };

            // Check if the requesting user has the require claims
 
            
                // Retrive targeted user exists from database
               User targetedUser = userManagementServices.FindUserbyUserName(targetedUserName);
            if (targetedUser == null)
            {
                throw new ArgumentException("There was no targeted user in database.");
            }
            // Check if the requesting user is  at least same level as  the targeted user

            else
            {
                userManagementServices.AddClaim(targetedUser, claim);
            }
        }

        /// <summary>
        /// Method to remove claim from user
        /// </summary>
        /// <param name="targetedUserName"></param>
        /// <param name="claim"></param>
        public void RemoveClaimAction(string targetedUserName, Claim claim)
        {
            if (targetedUserName == null)
            {
                throw new ArgumentNullException("targetedUserName");
            }
            else if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            // List of required claims needed for AddClaimAction Method
            List<Claim> createUserRequiredClaimTypes = new List<Claim>
            {

                new Claim("UserManager")
            };

            // Check if the requesting user has the require claims

                // Retrive targeted user exists from database
                User targetedUser = userManagementServices.FindUserbyUserName(targetedUserName);
            if (targetedUser == null)
             {
               throw new ArgumentException("There was no targeted user in database.");
             }
                // Check if the requesting user is  at least same level as  the targeted user
            else
            {
                userManagementServices.RemoveClaim(targetedUser, claim);
            }
                   
        }

        public void ChangePassword(String userName, String newPassword)
        {
            User user = userManagementServices.FindUserbyUserName(userName);
            user.HashPassword = newPassword;
            userManagementServices.UpdateUser(user);
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
