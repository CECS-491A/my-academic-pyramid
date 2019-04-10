
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
        public HttpResponseMessage Login(SSOPayload payload)
        {
            //CreateUsers();
            SessionManager sm = new SessionManager();
            UserManager um = new UserManager();
            Dictionary<string, string> redirectResponseDictionary 
                = new Dictionary<string, string>()
            {
                {"redirectURL", "https://myacademicpyramid.com/api/home" }
            };
            // Assume it's there for now.
            if (!sm.ValidateSSOPayload(payload))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            User user = um.FindByUserName(payload.Email);
            if (user == null)
            {
                UserDTO userDto = new UserDTO()
                {
                    UserName = payload.Email,
                    Email = payload.Email
                };
                um.CreateUserAccount(userDto);
                user = um.FindUserByEmail(userDto.Email);
            }
            string token = sm.CreateSession(user.Id);
            redirectResponseDictionary["redirectURL"] 
                = redirectResponseDictionary["redirectURL"] + "?token=" + token;
            return Request.CreateResponse(HttpStatusCode.OK, redirectResponseDictionary);
            
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

            UserDTO user4 = new UserDTO()
            {
                UserName = "julianpoyo+22@gmail.com",
                FirstName = "Julian",
                LastName = "Pollo",
                Email = "julianpoyo+22@gmail.com"
            };

            DatabaseContext db = new DatabaseContext();
            UserManager uM = new UserManager();
            uM.CreateUserAccount(user1);
            uM.CreateUserAccount(user2);
            uM.CreateUserAccount(user3);
            uM.CreateUserAccount(user4);
            db.SaveChanges();
        }
    }
}
