using System;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using DemoProject.Helper;
using ManagerLayer.UserManagement;
using SecurityLayer.Authorization.AuthorizationManagers;
using ServiceLayer.PasswordChecking.HashFunctions;


namespace DemoProject
{
    class ProgramUserManager
    {
        static void Main(string[] args)
        {

           
            //TestingHelper.ClearDatabase();

            //var UserManager = new UserManager();
            //SHA256HashFunction HashFunction = new SHA256HashFunction();
            //HashSalt hashSaltPassword = HashFunction.GetHashValue("Luis");

            //Console.WriteLine("Create Admin Account");
            //UserManager.CreateUserAccount(new UserDTO {
            //    UserName = "Admin",
            //    FirstName = "Luis",
            //    LastName = "Luin",
            //    Catergory = "Admin",
            //    BirthDate = DateTime.Today,
            //    RawPassword = "PasswordLuis",
            //    Location = "Long Beach",
            //    Email = "Luis@gmail.com",
            //    PasswordQuestion1 = "What is our favourite food ?",
            //    PasswordQuestion2 = "Where is your school?",
            //    PasswordQuestion3 = "What is your major",
            //    PasswordAnswer1 = "Burger",
            //    PasswordAnswer2 = "CSULB",
            //    PasswordAnswer3 = "CS",
            //} );

            //Console.WriteLine("Create SubAdmin Account");
            //UserManager.CreateUserAccount(new UserDTO
            //{
            //    UserName = "SubAdmin",
            //    FirstName = "Trong",
            //    LastName = "Luin",
            //    Catergory = "SubAdmin",
            //    BirthDate = DateTime.Today,
            //    RawPassword = "PasswordTrong",
            //    Location = "Long Beach",
            //    Email = "nguyentrong56@gmail.com",
            //    PasswordQuestion1 = "What is our favourite food ?",
            //    PasswordQuestion2 = "Where is your school?",
            //    PasswordQuestion3 = "What is your major",
            //    PasswordAnswer1 = "Burger",
            //    PasswordAnswer2 = "CSULB",
            //    PasswordAnswer3 = "CS",
            //});

            //Console.WriteLine("Assigning SubAdmin to Admin");
            //UserManager.AssignUserToUser("SubAdmin", "Admin");

            //UserManager.CreateUserAccount(new UserDTO
            //{
            //    UserName = "User1",
            //    FirstName = "Kevin",
            //    LastName = "NA",
            //    Catergory = "User",
            //    BirthDate = DateTime.Today,
            //    RawPassword = "PasswordKevin",
            //    Location = "Long Beach",
            //    Email = "Kevin@gmail.com",
            //    PasswordQuestion1 = "What is our favourite food ?",
            //    PasswordQuestion2 = "Where is your school?",
            //    PasswordQuestion3 = "What is your major",
            //    PasswordAnswer1 = "Burger",
            //    PasswordAnswer2 = "CSULB",
            //    PasswordAnswer3 = "CS",
            //});

            //Console.WriteLine("Assigning user to SubAdmin");
            //UserManager.AssignUserToUser("User1", "Admin");

            //Console.WriteLine("Testing if User1 has right to delete admin");
            //AuthorizationManager aM = new AuthorizationManager();

            //User user1 = UserManager.FindByUserName("User1");
            //User admin = UserManager.FindByUserName("Admin");
            
            //if(aM.HasHigherPrivilege(user1, admin))
            //{
            //    UserManager.DeleteUserAccount(admin);
            //}

            //else
            //{
            //    Console.WriteLine("User1 does not have enought privilge to delete Admin");
            //}


            //Console.WriteLine("Testing if Admin has right to delete User1");

            //if (aM.HasHigherPrivilege(admin, user1))
            //{
            //    UserManager.DeleteUserAccount(user1);
            //    Console.WriteLine("Delete Sucessfully");
            //}

            //else
            //{
            //    Console.WriteLine("User1 does not have enought privilge to delete Admin");
            //}

            //Console.WriteLine("Create another User called User2 and assigns it to SubAdmin");
            //UserManager.CreateUserAccount(new UserDTO
            //{
            //    UserName = "User2",
            //    FirstName = "Arturo",
            //    LastName = "NA",
            //    Catergory = "User",
            //    BirthDate = DateTime.Today,
            //    RawPassword = "PasswordArturo",
            //    Location = "Long Beach",
            //    Email = "Arturo@gmail.com",
            //    PasswordQuestion1 = "What is our favourite food ?",
            //    PasswordQuestion2 = "Where is your school?",
            //    PasswordQuestion3 = "What is your major",
            //    PasswordAnswer1 = "Burger",
            //    PasswordAnswer2 = "CSULB",
            //    PasswordAnswer3 = "CS",
            //});
            //UserManager.AssignUserToUser("User2", "SubAdmin");

            //Console.WriteLine("Testing if User2 has right to delete SubAdmin");
            //User subAdmin = UserManager.FindByUserName("SubAdmin");
            //User user2 = UserManager.FindByUserName("User2");


            //if (aM.HasHigherPrivilege(user2, subAdmin))
            //{
            //    UserManager.DeleteUserAccount(subAdmin);
            //    Console.WriteLine("Delete Sucessfully");
            //}

            //else
            //{
            //    Console.WriteLine("User2 does not have enought privilge to delete Subdmin");
            //}

            //Console.WriteLine("Make SubAdmin to be a child of User2 ");
            //UserManager.AssignUserToUser("SubAdmin", "User2");
            //subAdmin = UserManager.FindByUserName("SubAdmin");
            //Console.WriteLine("Testing if User2 has right to delete SubAdmin");



            //if (aM.HasHigherPrivilege(user2, subAdmin))
            //{
            //    UserManager.DeleteUserAccount(subAdmin);
            //    Console.WriteLine("Delete Sucessfully");
            //}

            //else
            //{
            //    Console.WriteLine("User2 does not have enought privilge to delete Subdmin");
            //}

            Console.ReadKey();
        }
    }
}
