using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization;

namespace UserManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var userManagement = new UserManagement.UserManagement<CustomUser>();

            userManagement.CreateUser(new CustomUser("Trong"));

            userManagement.AddClaim("Trong", "AccountManager");
            Console.ReadKey();
        }
    }
}
