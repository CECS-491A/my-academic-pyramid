using System;
using System.Data.Entity;
using DataAccessLayer;
using ManagerLayer.UserManagement;
using ServiceLayer.PasswordChecking.HashFunctions;
using  ServiceLayer.UserManagement.UserAccountServices;



namespace DemoProject
{
     class ProgramUMService
    {
        static void Main(string[] args)
        {

            DatabaseContext _DbContext = new DatabaseContext();
            var UserManager = new UserManager(_DbContext);
            //Create a new user account
            //Console.WriteLine("Create new user account - Trong");

            //SHA256HashFunction HashFunction = new SHA256HashFunction();
            //String userPassword = "Trong@90";
            //String hashedPassword = HashFunction.GetHashValue(userPassword);
            //PasswordQA passwordQA = new PasswordQA("What's your name", "Me", "What is your dog name", "Fox", "what is your heihgt", "5.09");
            //User newUser1 = new User("Trong", passwordQA);
            //UserManager.CreateUserAction(newUser1, hashedPassword);

            UserManager.DeleteUserAction("Trong");
            UserManager.DeleteUserAction("Trang");



            _DbContext.SaveChanges();



            //String userPassword2 = "Trong@93";
            //String hashedPassword2 = HashFunction.GetHashValue(userPassword2);
            //User newUser2 = new User("Cindy");
            //UserManager.CreateUserAction(new User("Cindy"), hashedPassword2);
            //UserManager.AssignUserToUser(newUser2, newUser1);


            Console.ReadKey();
        }
    }
}
