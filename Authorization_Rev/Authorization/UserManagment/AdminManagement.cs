using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class AdminManagement : IAdminManagement
    {
        

        public void DeleteOtherAccount()
        {
            Console.WriteLine("DeleteAccount is allowed");
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
    }
}
