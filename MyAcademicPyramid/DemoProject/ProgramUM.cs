using System;
using DataAccessLayer;
using DataAccessLayer.UserManagement.UserAccountServices;
using DataAccessLayer.Repository;
using Moq;
using System.Data.Entity;


namespace DemoProject
{
    class ProgramUM
    {
        static void Main(string[] args)
        {

            var mockUserSet = new Mock<DbSet<User>>();
            var mockClaimSet = new Mock<DbSet<Claim>>();

            var mockContext = new Mock<DatabaseContext>();
            mockContext.Setup(m => m.Users).Returns(mockUserSet.Object);
            mockContext.Setup(m => m.Claims).Returns(mockClaimSet.Object);
            var userManagement = new UserManagement(mockContext.Object);

            // Create a new user account
            Console.WriteLine("Create new user account - Trong");
            userManagement.CreateUser(new User("Trong"));

            // Search for the user account which is just created 
            User user = userManagement.FindUserbyUserName("Trong");

            // Add new claim to the account 
            Console.WriteLine("Add new claim to Trong ");
            Claim claim = new Claim("ManageAccount");
            userManagement.AddClaim(user, claim);
            


            // Remove claim from the account
            Console.WriteLine("Trying to remove a claim from account");
            userManagement.RemoveClaim(user, claim);

           

            //Try to delete user account

            Console.WriteLine("Trying to delete user");
            userManagement.DeleteUser(user);
            Console.ReadKey();
        }
    }
}
