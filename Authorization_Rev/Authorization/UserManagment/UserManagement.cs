using Authorization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class UserManagement : IUserManagement
    {
        
        public UserManagement()
        {
            
        }


        public void DeleteOtherAccount()
        {

            Console.WriteLine("DeleteOtherAccount is allowed");
        }

        public void DeleteUserPost()
        {
            Console.WriteLine("DeleteUserPost is allowed");
        }

        public void DisableUser()
        {
            Console.WriteLine("DisableUser is allowed");
        }

        public void EnableUser()
        {
            Console.WriteLine("EnableUser is allowed");
        }

        public void DeleteUserOwnAccount()
        {
            Console.WriteLine("DeleteUserOwnAccount is allowed");

        }
    }
}
