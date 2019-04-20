using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using WebAPI.UserManagement;
using System.Web.Http.Cors;
using SecurityLayer.Sessions;
using ManagerLayer.Constants;
using DataAccessLayer.Models;

namespace KFC.SIT.WebAPI.Controllers
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
                    Email = payload.Email,
                    Category = "NewUser"
                };
                um.CreateUserAccount(userDto);
                user = um.FindByUserName(payload.Email);
                um.AddClaimAction(user.Id, new Claim("CanRegister"));
            }
            string token = sm.CreateSession(user.Id);

            Dictionary<string, string> redirectResponseDictionary
                = new Dictionary<string, string>()
            {
                {"redirectURL", RedirectUserUtility.GetUrlAddress(user.Category.Value) }
            };
            redirectResponseDictionary["redirectURL"] 
                = redirectResponseDictionary["redirectURL"] + "?SITtoken=" + token;
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
