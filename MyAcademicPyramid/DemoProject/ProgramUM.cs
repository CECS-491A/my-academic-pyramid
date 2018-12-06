using System;
using DataAccessLayer;
using DataAccessLayer.UserManagement.UserAccountServices;



namespace DemoProject
{
     class ProgramUM
    {
        static void Main(string[] args)
        {

            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            var userManagement = new UserManagement();

            // Create a new user account
            Console.WriteLine("Create new user account - Trong");
            userManagement.CreateUser(new User("Trong"));

            // Search for the user account which is just created 
            User user = userManagement.FindUserbyUserName("Trong");

            // Add new claim to the account 
            Console.WriteLine("Add new claim to Trong ");
            Claim claim = new Claim("ManageAccount");
            userManagement.AddClaim(user, claim);

            Console.WriteLine("Remove the claim from Trong ");
            user = userManagement.FindUserbyUserName("Trong");

            // Remove claim from the account
            Console.WriteLine("Trying to remove a claim from account");
            userManagement.RemoveClaim(user, claim);

            //Try to retrive the user again
            user = userManagement.FindUserbyUserName("Trong");

            //Try to delete user account

            Console.WriteLine("Trying to delete user");
            userManagement.DeleteUser(user);
            Console.ReadKey();
        }
    }
}
