using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using WebAPI.UserManagement;
using SecurityLayer.Sessions;
using DataAccessLayer.Models;

namespace KFC.SIT.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : ApiController
    {
        [HttpGet]
        public String Hello()
        {
            return "Hello World From Backend API " + DateTime.Now;
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

        [HttpPost]
        //[Route("Home/TestJWT")]
        public string TestJWT([FromBody] string username)
        {

            /* Create three users. 
             * Create 
             */

            CreateUsers();
            DatabaseContext db = new DatabaseContext();
            SessionManager sm = new SessionManager();
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
                return sm.CreateSession(user.Id);
            }
            

            //string token = tm.GenerateToken();
            
    //Dictionary<string, string> testDict = new Dictionary<string, string>()
    //{
    //    {"User", "ljulian2190@gmail.com" },
    //    {"Claims", "[Student, Tutor]" }
    //};


    //return JWTokenManager.GenerateToken(testDict);
    }


    }
}
