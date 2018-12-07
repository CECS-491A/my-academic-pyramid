using DataAccessLayer;
using System;
using System.Collections.Generic;
using SecurityLayer.Authorization.AuthorizationManagers;

namespace DemoProject.MockUserManagementNameSpace
{
    public class UserManagementController
    {
        /*
         * Class that demonstrates how authorization class might be used in
         * a controller.
         * */
        IUserManagement userManagement = null;
        public UserManagementController()
        {
            userManagement = new MockUserManagement();
        }

        public void DeleteOwnAction(User requestingUser)
        {
            IAuthorizationManager authManager = new AuthorizationManager(requestingUser);
            List<Claim> delOwnRequiredClaimTypes = new List<Claim>
            {
                new Claim("CanDeleteUserOwnAccount")
            };
            if (authManager.CheckClaims(delOwnRequiredClaimTypes))
            {
                userManagement.DeleteUserOwnAccount();
            }
            else
            {
                Console.WriteLine("DeleteUserOwnAccount is BLOCKED");
            }
        }

        public void DeleteOtherAction(User requestingUser)
        {
            IAuthorizationManager authManager = new AuthorizationManager(requestingUser);
            List<Claim> delOtherRequiredClaimTypes = new List<Claim>
            {
                new Claim("CanDeleteOtherAccount"),
                new Claim("HasPoints")
            };
            // delOtherRequiredClaimTypes = null;
            if (authManager.CheckClaims(delOtherRequiredClaimTypes))
            {
                userManagement.DeleteOtherAccount();
            }
            else
            {
                Console.WriteLine("DeleteOtherAccount is BLOCKED");
            }
        }

        public void DisableUserAction(User requestingUser)
        {
            IAuthorizationManager authManager = new AuthorizationManager(requestingUser);
            List<Claim> disableUserRequiredClaimTypes = new List<Claim>
            {
                new Claim("CanDisableUser")
            };
            if (authManager.CheckClaims(disableUserRequiredClaimTypes))
            {
                userManagement.DisableUser();
            }
            else
            {
                Console.WriteLine("DisableUser is BLOCKED");
            }
        }

        public void EnableUserAction(User requestingUser)
        {
            IAuthorizationManager authManager = new AuthorizationManager(requestingUser);
            List<Claim> enableUserRequiredClaimTypes = new List<Claim>
            {
                new Claim("CanEnableUser"),
            };
            if (authManager.CheckClaims(enableUserRequiredClaimTypes))
            {
                userManagement.EnableUser();
            }
            else
            {
                Console.WriteLine("EnableUser is BLOCKED");
            }
        }
    }
}
