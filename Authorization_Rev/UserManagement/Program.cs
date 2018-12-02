using System;
using Authorization;

namespace UserManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var userManagement = new UserManagement<CustomUser>();

            userManagement.CreateUser(new CustomUser("Trong"));

            userManagement.AddClaim("Trong", "AccountManager");
            Console.ReadKey();
        }
    }
}
