using DataAccessLayer;
using System;
using System.Collections.Generic;
using SecurityLayer.Authorization.AuthorizationManagers;
using ServiceLayer.UserManagement.UserAccountServices;

namespace ManagerLayer.UserManagement
{
    public class UserManager
    {
        /*
         * Class that demonstrates how authorization class might be used in
         * a controller.
         * */
        private UserManagementServices userManagement = null;
        private User _requestingUser;
        private User _targetedUser;
        private UnitOfWork uOW = new UnitOfWork();

        // Constructor that accept a username of account and initiaize the UserManagementServices
        // User Account has to exist in database 
        public UserManager(String requestingUserName )
        {
            if (requestingUserName == null)
            {
                throw new ArgumentNullException("requestingUserName");
            }
            // Call UserManagementServices 
            userManagement = new UserManagementServices(uOW);

            // Find the user account in database and assign it to data member _requestingUser
            // _requestingUser  will be used to compare with targeted User parameter for Delete method  
            _requestingUser = userManagement.FindUserbyUserName(requestingUserName);
            if (_requestingUser == null)
            {
                throw new ArgumentNullException("_requestingUser", "User has to exist in database.");
            }
           
        }

        // Special overload constructor which is only used to create System Admin for testing
        // The constructor will take a new username and a boolean true  to create a very first System Admin (Like the root of the tree)
        public UserManager(String newSystemAdminUserName, bool asSystemAdmin)
        {
            if(newSystemAdminUserName == null)
            {
                throw new ArgumentNullException("newSystemAdminUserName");
            }
            if(asSystemAdmin ==true)
            {
                // Call UserManagementServices 
                userManagement = new UserManagementServices(uOW);
                userManagement.CreateUser(new User (newSystemAdminUserName));
                _requestingUser = userManagement.FindUserbyUserName(newSystemAdminUserName);
                userManagement.AddClaim(_requestingUser,new Claim ("UserManager"));
                

            }

            else
            {
                throw new ArgumentException(
                    "asSystemAdmin", "Variable need to be true to create a System Admin."
                );
            }
        }

        // Method to delete self user account or other account
        public void DeleteAction(String targetedUserName)
        {
            if (targetedUserName == null)
            {
                throw new ArgumentNullException("targetedUserName");
            }

            // Call AuthorizationManager and pass the requesting user object in
            IAuthorizationManager authManager = new AuthorizationManager(_requestingUser);
            // Retrieve the target user object from database which delete action applies on 
            _targetedUser = userManagement.FindUserbyUserName(targetedUserName);
            if (_targetedUser == null)
            {
                throw new ArgumentNullException(
                    "_targetedUser", "The user to be deleted doesn't exist."
                );
            }

            // List of required claims needed for DeleteUser Method
            List<Claim> delOtherRequiredClaimTypes = new List<Claim>
            {
                new Claim("UserManager"),
   
            };
            
            // Check if the requesting user has the require claims
            if (authManager.CheckClaims(delOtherRequiredClaimTypes))
            {
                    // Check if the requesting user is  at least same level  as  the targeted user   
                    if (authManager.HasHigherPrivilege(_requestingUser, _targetedUser))
                    { 
                        userManagement.DeleteUser(_targetedUser);
                    }
                    else
                    {
                        throw new ArgumentException(
                            "ERROR--Calling user isn't of a higher level as targeted user."
                        );
                    }
            }
            else
            {
                throw new ArgumentException(
                    "ERROR--DeleteOtherAccount action is BLOCKED because the required claims are not meet"
                );
            }
        }

        // Method to create user account
        public void CreateUserAction(String targetedUserName)
        {
            if (targetedUserName == null)
            {
                throw new ArgumentNullException("targetedUserName");
            }
            // Call AuthorizationManager and pass the requesting user object in
            IAuthorizationManager authManager = new AuthorizationManager(_requestingUser);

            // List of required claims needed for CreateUser Method
            List<Claim> createUserRequiredClaimTypes = new List<Claim>
            {

                new Claim("UserManager")
            };

            // Check if the requesting user has the require claims
            if (authManager.CheckClaims(createUserRequiredClaimTypes))
            {
                // Create user object
                _targetedUser = new User(targetedUserName);

                // Assign newly created user account with the ID the User who make request as parentID
                _targetedUser.ParentUser = _requestingUser;
                

                // Tell userManagement services to create user in database 
                userManagement.CreateUser(_targetedUser);
                
            }
            else
            {
                throw new ArgumentException(
                    "ERROR--Create Account action is BLOCKED because the required claims are not meet"
                );
            }
        }

        // Method to return an user object by lookup the user name
        public User FindUserAction(String userName)
        {
            // Call AuthorizationManager and pass the requesting user object in
            try
            {
                IAuthorizationManager authManager = new AuthorizationManager(_requestingUser);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            return userManagement.FindUserbyUserName(userName);
        
        }

        // Method to print all users in database 
        public void  PrintAllUser()
        {
            // Call GetAllUser method in userManagementServices 
            List<User> userList = userManagement.GetAllUser();

            foreach(User user in userList)
            {
                Console.WriteLine(user.UserName);
            }
        }

        // Method to add claim to user account
        // The method will take username of the account whom give the claim to and a claim onje
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
            IAuthorizationManager authManager = new AuthorizationManager(_requestingUser);

            // List of required claims needed for AddClaimAction Method
            List<Claim> createUserRequiredClaimTypes = new List<Claim>
            {

                new Claim("UserManager")
            };

            // Check if the requesting user has the require claims
            if (authManager.CheckClaims(createUserRequiredClaimTypes))
            {
                // Retrive targeted user exists from database
                _targetedUser = userManagement.FindUserbyUserName(targetedUserName);
                if (_targetedUser == null)
                {
                    throw new ArgumentException("There was no targeted user in database.");
                }
                // Check if the requesting user is  at least same level as  the targeted user
                if (authManager.HasHigherPrivilege(_requestingUser, _targetedUser))
                {
                    userManagement.AddClaim(_targetedUser, claim);
                }
            }

            else
            {
                throw new ArgumentException(
                    "ERROR--Add Claim action is BLOCKED because the required claims are not meet"
                );
            }

        }



        


            //public void DisableUserAction(User targetedUser)
            //{
            //    IAuthorizationManager authManager = new AuthorizationManager(targetedUser);
            //    List<Claim> disableUserRequiredClaimTypes = new List<Claim>
            //    {
            //        new Claim("CanDisableUser")
            //    };
            //    if (authManager.CheckClaims(disableUserRequiredClaimTypes))
            //    {
            //        userManagement.DisableUser();
            //    }
            //    else
            //    {
            //        Console.WriteLine("DisableUser is BLOCKED");
            //    }
            //}

            //public void EnableUserAction(User targetedUser)
            //{
            //    IAuthorizationManager authManager = new AuthorizationManager(targetedUser);
            //    List<Claim> enableUserRequiredClaimTypes = new List<Claim>
            //    {
            //        new Claim("CanEnableUser"),
            //    };
            //    if (authManager.CheckClaims(enableUserRequiredClaimTypes))
            //    {
            //        userManagement.EnableUser();
            //    }
            //    else
            //    {
            //        Console.WriteLine("EnableUser is BLOCKED");
            //    }
            //}
        }
}
