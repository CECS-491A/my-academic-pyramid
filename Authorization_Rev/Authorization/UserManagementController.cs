using Authorization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    class UserManagementController
    {
        IUserManagement userManagement = null;
        public UserManagementController()
        {
            userManagement = new UserManagement();
        }

        public void DeleteOwnAction(User requestingUser)
        {
            IAuthorizationManager authManager = new AuthorizationManager(requestingUser);
            List<string> delOwnRequiredClaimTypes = new List<string>
            {
                "CanDeleteUserOwnAccount"
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
            List<string> delOtherRequiredClaimTypes = new List<string>
            {
                "CanDeleteOtherAccount",
                "HasPoints"
            };
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
            List<string> disableUserRequiredClaimTypes = new List<string>
            {
                "CanDisableUser"
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
            List<string> enableUserRequiredClaimTypes = new List<string>
            {
                "CanEnableUser"
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
