using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using WebAPI.Gateways.UserManagement;
using System.Web.Http.Cors;
using SecurityLayer.Sessions;
using ManagerLayer.Constants;
using DataAccessLayer.Models;
using KFC.SIT.WebAPI.Utility;
using ManagerLayer.sso;
using System;

namespace KFC.SIT.WebAPI.Controllers
{ 
    public class LoginController : ApiController
    {
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult Login(SsoPayload payload)
        {
            //CreateUsers();
            SessionManager sm = new SessionManager();
            UserManager um = new UserManager();
            string URL_FIRST_PART 
                = $"{WebAPIConstants.FRONT_END_LOCAL}/#/Redirect";
            
            // Assume it's there for now.
            if (!SignatureService.IsValidClientRequest(
                    payload.SSOUserId, payload.Email, long.Parse(payload.Timestamp), 
                    payload.Signature
                ))
            {
                return Unauthorized();
            }

            Account user = um.FindByUserName(payload.Email);
            if (user == null)
            {
                UserDTO userDto = new UserDTO()
                {
                    UserName = payload.Email,
                    SsoId = new Guid(payload.SSOUserId),
                    Email = payload.Email,
                    Category = "NewUser"
                };
                um.CreateUserAccount(userDto);
                user = um.FindByUserName(payload.Email);
                um.AddClaimAction(user.Id, new Claim("CanRegister"));
                um.AddClaimAction(user.Id, new Claim("CanReadOwnStudentAccount"));
            }
            string token = sm.CreateSession(user.Id);

            string redirectUrl = URL_FIRST_PART + "?SITtoken=" + token;
            // For production
            //return Redirect(redirectUrl);

            //Local only
            Dictionary<string, string> redirectResponseDictionary = new Dictionary<string, string>()
            {
                { "redirectURL", URL_FIRST_PART }
            };
            redirectResponseDictionary["redirectURL"]
                         = redirectResponseDictionary["redirectURL"] + "?SITtoken=" + token;
            return Ok(redirectResponseDictionary);

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
