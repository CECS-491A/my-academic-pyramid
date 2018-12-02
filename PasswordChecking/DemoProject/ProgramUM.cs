using System;
using ServiceLayer;
using ServiceLayer.UserManagement.UserAccountServices;

namespace ManagerLayer
{
    class ProgramUM
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
