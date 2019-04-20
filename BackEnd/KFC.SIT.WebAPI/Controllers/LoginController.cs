
using ServiceLayer.PasswordChecking.HashFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SecurityLayer;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using WebAPI.UserManagement;

namespace KFC.SIT.WebAPI
{ 
    public class LoginController : ApiController
    {
        [HttpPost]
        public string Login([FromBody]string username)
        {
            CreateUsers();
            DatabaseContext db = new DatabaseContext();
            JWTokenManager tm = new JWTokenManager(db);
            UserManager um = new UserManager();
            User user = um.FindByUserName(username);
            if (user == null)
            {
                return "User with that username not found";
            }
            else
            {
                Dictionary<string, string> testPayload = new Dictionary<string, string>()
                {
                    { "a", "1"},
                    { "b", "2" },
                    { "c", "3" }
                };
                string token = tm.GenerateToken(user.Id, testPayload);
                return token;
            }



        }

        private void CreateUsers()
        {
            UserDTO user1 = new UserDTO()
            {
                UserName = "Abc@gmail.com",
                FirstName = "Jackie",
                LastName = "Chan",
                
            };

            UserDTO user2 = new UserDTO()
            {
                UserName = "tri@yahoo.com",
                FirstName = "David",
                LastName = "Gonzales",
                
            };

            UserDTO user3 = new UserDTO()
            {
                UserName = "Smith@gmail.com",
                FirstName = "Michael",
                LastName = "Nguyen",
               
            };

            DatabaseContext db = new DatabaseContext();
            UserManager uM = new UserManager();
            uM.CreateUserAccount(user1);
            uM.CreateUserAccount(user2);
            uM.CreateUserAccount(user3);
            db.SaveChanges();
        }
    }
}
