
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
using ManagerLayer.UserManagement;
using System.Web.Http.Cors;
using SecurityLayer.Sessions;

namespace KFC.SIT.WebAPI
{ 
    public class LoginController : ApiController
    {
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Login(SSOPayload payload)
        {
            //CreateUsers();
            //DatabaseContext db = new DatabaseContext();
            //JWTokenManager tm = new JWTokenManager(db);
            SessionManager sm = new SessionManager();
            UserManager um = new UserManager();
            // Assume it's there for now.
            User user = um.FindByUserName(payload.Email);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                // TODO do signature validation.
                Dictionary<string, string> testPayload = new Dictionary<string, string>()
                {
                    { "a", "1"},
                    { "b", "2" },
                    { "c", "3" }
                };
                string token = sm.CreateSession(user.Id);
                return Ok(token);
            }
        }

        private void CreateUsers()
        {
            UserDTO user1 = new UserDTO()
            {
                UserName = "Abc@gmail.com",
                FirstName = "Jackie",
                LastName = "Chan",
                Email = "Abc@gmail.com"
            };

            UserDTO user2 = new UserDTO()
            {
                UserName = "tri@yahoo.com",
                FirstName = "David",
                LastName = "Gonzales",
                Email = "tri@yahoo.com"
            };

            UserDTO user3 = new UserDTO()
            {
                UserName = "Smith@gmail.com",
                FirstName = "Michael",
                LastName = "Nguyen",
                Email = "Smith@gmail.com"
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
