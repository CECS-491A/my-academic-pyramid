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
        public UserManagementController(User requestingUser )
        {

            userManagement = new UserManagement();
            _requestingUser = requestingUser;
        }

        public void DeleteOwnAction(User targetedUser)
        {
            IAuthorizationManager authManager = new AuthorizationManager(_requestingUser);
            List<Claim> delOwnRequiredClaimTypes = new List<Claim>
            {
                new Claim("CanDeleteUserOwnAccount")
            };
            if (authManager.CheckClaims(delOwnRequiredClaimTypes))
            {
                
                userManagement.DeleteUser(targetedUser);
            }
            else
            {
                Console.WriteLine("DeleteUserOwnAccount is BLOCKED");
            }
        }

        public void DeleteOtherAction(User targetedUser)
        {
            IAuthorizationManager authManager = new AuthorizationManager(_requestingUser);
            List<Claim> delOtherRequiredClaimTypes = new List<Claim>
            {
                new Claim("CanDeleteOtherAccount"),
                new Claim("HasPoints")
            };
        

            // delOtherRequiredClaimTypes = null;
            if (authManager.CheckClaims(delOtherRequiredClaimTypes))
            {

                if (authManager.DeletedUserIsChild(targetedUser))
                    {
                    userManagement.DeleteUser(targetedUser);
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


        public void CreateUserAction(User targetedUser)
        {
            IAuthorizationManager authManager = new AuthorizationManager(_requestingUser);
            //List<Claim> createUserRequiredClaimTypes = new List<Claim>
            //{
            
            //    new Claim("UserManager")
            //};
            // delOtherRequiredClaimTypes = null;
            //if (authManager.CheckClaims(createUserRequiredClaimTypes))
            //{
                
                userManagement.CreateUser(targetedUser);
                targetedUser = userManagement.FindUserbyUserName(targetedUser.UserName);

                _requestingUser = userManagement.FindUserbyUserName(_requestingUser.UserName);
                targetedUser.User1 = _requestingUser;

                userManagement.UpdateUser(targetedUser);

                _requestingUser.Users1.Add(targetedUser);

                userManagement.UpdateUser(_requestingUser);
                

            //}
            //else
            //{
            //    Console.WriteLine("Create Account is BLOCKED");
            //}
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
