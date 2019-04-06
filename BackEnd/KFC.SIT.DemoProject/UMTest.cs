using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using DemoProject.Helper;
using ManagerLayer.UserManagement;
using SecurityLayer.Authorization.AuthorizationManagers;
using ServiceLayer.PasswordChecking.HashFunctions;

namespace DemoProject
{
    class UMTest
    {
        static void Main(String[] args)
        {
            UserManager UM = new UserManager();
            User createdUser = UM.CreateUserAccount(new UserDTO
            {
                Id = 100,
                UserName = "SystemAdmin",
                FirstName = "Arturo",
                LastName = "NA",
                Catergory = new Catergory("User"),
                //BirthDate = DateTime.UtcNow,
                RawPassword = "PasswordArturo",
                //Location = "Long Beach",
                
            });
            bool expected = true;
            bool actual;

            // Act
            try
            {
                User foundUser = UM.FindUserById(100);
                Console.WriteLine(foundUser.Email);
                if (foundUser == createdUser)
                {
                    actual = true;
                }
                else
                {
                    actual = false;
                }
            }
            catch (ArgumentNullException)
            {
                actual = false;
            }
            Console.ReadKey();
        }
    }
}
