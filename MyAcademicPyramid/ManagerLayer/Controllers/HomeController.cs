using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using SecurityLayer;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using ManagerLayer.UserManagement;
using ServiceLayer.PasswordChecking.HashFunctions;

namespace ManagerLayer.Controllers
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
            User user1 = new User()
            {
                UserName = "Abc@gmail.com",
                Firstname = "Jackie",
                LastName = "Chan"
            };

            User user2 = new User()
            {
                UserName = "tri@yahoo.com",
                Firstname = "David",
                LastName = "Gonzales"
            };

            User user3 = new User()
            {
                UserName = "Smith@gmail.com",
                Firstname = "Michael",
                LastName = "Nguyen"
            };

            DatabaseContext db = new DatabaseContext();
            UserManager uM = new UserManager(db);
            uM.CreateUserAction(user1, "eoifj");
            uM.CreateUserAction(user2, "eoifj");
            uM.CreateUserAction(user3, "eoifj");
            db.SaveChanges();
        }

        [HttpGet]
        [Route("Home/TestJWT")]
        public string TestJWT([FromBody] string username)
        {

            /* Create three users. 
             * Create 
             */

            CreateUsers();
            DatabaseContext db = new DatabaseContext();
            JWTokenManager tm = new JWTokenManager(db);

            //string token = tm.GenerateToken();
            //return Ok(user);
            return "";
    //Dictionary<string, string> testDict = new Dictionary<string, string>()
    //{
    //    {"User", "ljulian2190@gmail.com" },
    //    {"Claims", "[Student, Tutor]" }
    //};


    //return JWTokenManager.GenerateToken(testDict);
    }


    }
}
