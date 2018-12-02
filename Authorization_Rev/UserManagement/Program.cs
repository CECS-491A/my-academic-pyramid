using System;
using Authorization;

namespace UserManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var userManagement = new UserManagement();

            userManagement.CreateUser(new User("Trong"));

            userManagement.AddClaim("Trong", "AccountManager");
            Console.ReadKey();
        }
    }
}
