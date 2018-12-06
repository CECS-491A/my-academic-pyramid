using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.UserManagement.UserAccountServices;

namespace DemoProject
{
    class ProgramUM
    {
        static void Main(string[] args)
        {
            var userManagement = new UserManagement();

            // Create a new user account
            Console.WriteLine("Create new user account - Trong");
            userManagement.CreateUser(new User("Trong", 1));

            // Search for the user account which is just created 
            User searchUser = userManagement.FindUserbyUserName("Trong");

            // Add new claim to the account 
            Console.WriteLine("Add new claim to Trong ");
            userManagement.AddClaim(searchUser, "AccountManager");
            
            // Search the account to make sure it is created successfully 
            Console.WriteLine("Trying to search Trong's account and print out the claims");
            searchUser = userManagement.FindUserbyUserName("Trong");
            Console.WriteLine($"Name: {searchUser.UserName}");

            // Get  claim belongs to the account
            Console.WriteLine("Trying to get claim from Trong's account");
            IList<String> claims = userManagement.GetClaims(searchUser);
            foreach (String claim in claims )
            {
                Console.WriteLine(claim);
            }

            // Remove claim from the account
            Console.WriteLine("Trying to remove a claim from account");
            userManagement.RemoveClaim(searchUser, "AccountManager");

            // Try to get the claim again to the account to make sure the claim was removed 
            Console.WriteLine("Trying to retrieve claim from the account again ");     
            claims = userManagement.GetClaims(searchUser);
            foreach (String claim in claims)
            {
                Console.WriteLine(claim);
            }

            //Try to delete user account

            Console.WriteLine("Trying to delete user");
            userManagement.DeleteUser(searchUser);
            Console.ReadKey();
        }
    }
}
