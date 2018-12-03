using System;
using DataAccessLayer;
using DataAccessLayer.UserManagement.UserAccountServices;

namespace ManagerLayer
{
    class ProgramUM
    {
        static void Main(string[] args)
        {
            var userManagement = new UserManagement();

            userManagement.CreateUser(new User("Trong", 1));

            User searchUser = userManagement.FindUserbyUserName("Trong");

            userManagement.AddClaim(searchUser, "AccountManager");
            Console.ReadKey();
        }
    }
}
