using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class UserManagement : AuthorizationController, IUserManagement
    {
        
        public UserManagement(User user) : base(user)
        {
            
        }


        public void DeleteOtherAccount()
        {
            if(checkClaim("CanDeleteOtherAccount"))
            {
               Console.WriteLine("DeleteOtherAccount is allowed");
            }
            else
            {
                Console.WriteLine("DeleteOtherAccount is BLOCKED");
            }
            
        }

        public void DeleteUserPost()
        {
            if (checkClaim("CanDeleteUserPost"))
            {
                Console.WriteLine("DeleteUserPost is allowed");
            }
            else
            {
                Console.WriteLine("DeleteUserPost is BLOCKED");
            }
        }

        public void DisableUser()
        {
            if (checkClaim("CanDisableUser"))
            {
                Console.WriteLine("DisableUser is allowed");
            }
            else
            {
                Console.WriteLine("DisableUser is BLOCKED");

            }
        }

        public void EnableUser()
        {
            if (checkClaim("CanEnableUser"))
            {
                Console.WriteLine("EnableUser is allowed");
            }
            else
            {
                Console.WriteLine("EnableUser is BLOCKED");

            }
        }

        public void DeleteUserOwnAccount()
        {
            if (checkClaim("CanDeleteUserOwnAccount"))
            {
                Console.WriteLine("DeleteUserOwnAccount is allowed");
            }
            else
            {
                Console.WriteLine("DeleteUserOwnAccount is BLOCKED");

            }

        }
    }
}
