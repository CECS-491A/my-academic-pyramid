using DataAccessLayer;
using System;
using System.Collections.Generic;
using SecurityLayer.Authorization.AuthorizationManagers;
using ServiceLayer.UserManagement.UserAccountServices;

namespace DemoProject.MockUserManagementNameSpace
{
    public class UserManagementController
    {
        /*
         * Class that demonstrates how authorization class might be used in
         * a controller.
         * */
        UserManagement userManagement = null;
        User _requestingUser;
        User _targetedUser;
        UnitOfWork uOW = new UnitOfWork();
        public UserManagementController(String requestingUserName )
        {
         
            
            userManagement = new UserManagement(uOW);
            _requestingUser = userManagement.FindUserbyUserName(requestingUserName);
           

        }

        public void DeleteOwnAction(String targetedUserName)
        {
            IAuthorizationManager authManager = new AuthorizationManager(_requestingUser);
            _targetedUser = userManagement.FindUserbyUserName(targetedUserName);
            List<Claim> delOwnRequiredClaimTypes = new List<Claim>
            {
                new Claim("CanDeleteUserOwnAccount")
            };
            if (authManager.CheckClaims(delOwnRequiredClaimTypes))
            {
                
                userManagement.DeleteUser(_targetedUser);
            }
            else
            {
                Console.WriteLine("DeleteUserOwnAccount is BLOCKED");
            }
        }

        public void DeleteOtherAction(String targetedUserName)
        {
            
            IAuthorizationManager authManager = new AuthorizationManager(_requestingUser);
            _targetedUser = userManagement.FindUserbyUserName(targetedUserName);
            List<Claim> delOtherRequiredClaimTypes = new List<Claim>
            {
                new Claim("UserManager"),
   
            };
        

            // delOtherRequiredClaimTypes = null;
            if (authManager.CheckClaims(delOtherRequiredClaimTypes))
            {
                
                if (authManager.DeletedUserIsChild(_targetedUser))
                    {
                    userManagement.DeleteUser(_targetedUser);
                }
                else
                {
                    Console.WriteLine("Cannot delete parent user");
                }
            }
            else
            {
                Console.WriteLine("DeleteOtherAccount is BLOCKED");
            }
        }


        public void CreateUserAction(String targetedUserName)
        {
            IAuthorizationManager authManager = new AuthorizationManager(_requestingUser);
            List<Claim> createUserRequiredClaimTypes = new List<Claim>
            {

                new Claim("UserManager")
            };
       
            if (authManager.CheckClaims(createUserRequiredClaimTypes))
            {
                _targetedUser = new User();
                _targetedUser.UserName = targetedUserName;



                _targetedUser.ParentUser = _requestingUser;
                userManagement.CreateUser(_targetedUser);
                
            }
            else
            {
                Console.WriteLine("Create Account is BLOCKED");
            }
        }


        public User FindUserAction(String userName)
        {
            IAuthorizationManager authManager = new AuthorizationManager(_requestingUser);


                return userManagement.FindUserbyUserName(userName);
        
  
        }


        public void addClaimAction(Claim claim)
        {
            IAuthorizationManager authManager = new AuthorizationManager(_requestingUser);


            userManagement.AddClaim(_requestingUser, claim);

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
