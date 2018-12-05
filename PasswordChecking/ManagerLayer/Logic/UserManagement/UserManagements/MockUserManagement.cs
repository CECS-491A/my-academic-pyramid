using System;

namespace ManagerLayer.Logic.UserManagement.UserManagements
{
    public class MockUserManagement : IUserManagement
    {

        public MockUserManagement()
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