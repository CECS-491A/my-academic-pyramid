using DataAccessLayer;
using DataAccessLayer.DTOs;
using ManagerLayer.UserManagement;
using ServiceLayer.PasswordChecking.HashFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ManagerLayer.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Login(UserDTO userDto)
        {
            DatabaseContext db = new DatabaseContext();
            UserManager um = new UserManager();

            User user = um.FindUserName(userDto.UserName);
            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The user was not found");
            }

            SHA256HashFunction HashFunction = new SHA256HashFunction();

            String salt = user.PasswordSalt;
            bool passwordValidation = UserManager.VerifyPassword(userDto.RawPassword, user.PasswordHash, user.PasswordSalt);

            if (!passwordValidation)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The username/password combination was wrong");

            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, user.UserName);
            }
            


        }
    }
}
