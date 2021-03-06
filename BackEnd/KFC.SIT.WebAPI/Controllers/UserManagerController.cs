﻿using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using WebAPI.Gateways.UserManagement;
using SecurityLayer.Sessions;
using ServiceLayer.PasswordChecking.HashFunctions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using KFC.SIT.WebAPI.Utility;
using SecurityLayer.Authorization;
using SecurityLayer.Authorization.AuthorizationManagers;
using ManagerLayer.sso;

namespace KFC.SIT.WebAPI.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserManagerController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public IQueryable<UserDTO> GetAllUsers()
        {
            UserManager umManager = new UserManager();
            IList<Account> userList = umManager.GetAllUser();

            List<UserDTO> list = new List<UserDTO>();
            foreach (var user in userList)
            {
                list.Add(new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Category = user.Category.Value,
                    DateOfBirth = user.DateOfBirth.ToString("MMMM dd yyyy hh:mm:ss tt"),
                    CreatedAt = user.CreatedAt.ToString("MMMM dd yyyy hh:mm:ss tt")


                });
            }

            return list.AsQueryable();
        }

        [HttpGet]
        [ActionName("GetContextId")]
        public HttpResponseMessage GetContextId()
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
            );
            if (securityContext == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            string updatedToken = sm.RefreshSession(securityContext.Token);
            // TODO finish this.
            return Request.CreateResponse(
                HttpStatusCode.OK, new { userid = securityContext.UserId, SITtoken = updatedToken }
            );
        }

        [HttpGet]
        [ActionName("GetUserInfoWithId")]
        public HttpResponseMessage GetUserInfoWithId(int id)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
           );
            if (securityContext == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanReadOwnStudentAccount"
            };

            if (securityContext.UserId != id ||
                !authorizationManager.CheckClaims(requiredClaims))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                UserManager um = new UserManager();
                UserDTO userDTO = um.GetUserInfo(id);
                var student = um.FindStudentById(id);
                if(student is null)
                {
                    userDTO.SchoolId = 0;
                }
                else
                {
                    userDTO.SchoolId = student.SchoolDepartment.SchoolId;
                }
                string updatedToken = sm.RefreshSession(securityContext.Token);
                return Request.CreateResponse(
                    HttpStatusCode.OK, new { User = userDTO, SITtoken = updatedToken }
                );
            }
        }

        [HttpGet]
        public IHttpActionResult GetPublicUserInfoWithId(int id)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
            );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanReadAStudentPublicInformation"
            };

            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                UserManager um = new UserManager();
                UserDTO userDTO = um.GetUserInfo(id);
                var publicUserInfo = new
                {
                    Id = userDTO.Id,
                    UserName = userDTO.UserName,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Exp = userDTO.Exp
                };
                string updatedToken = sm.RefreshSession(securityContext.Token);
                return Ok(
                    new { User = publicUserInfo, SITtoken = updatedToken }
                );
            }
        }


        // POST api/<controller>
        [HttpPost]

        public HttpResponseMessage CreateNewUSer([FromBody] UserDTO userDto)
        {
            UserManager umManager = new UserManager();
            var createdUser = umManager.CreateUserAccount(userDto);
            var message = Request.CreateResponse(HttpStatusCode.OK, userDto);

            return message;

        }

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult EditUser([FromBody] UserDTO userDto)
        {
            UserManager umManager = new UserManager();
            Account foundUser = umManager.FindUserById(userDto.Id);
            foundUser.UserName = userDto.UserName;
            foundUser.FirstName = userDto.FirstName;
            foundUser.LastName = userDto.LastName;
            return Ok(umManager.UpdateUserAccount(foundUser));
        }


        [HttpDelete]
        public IHttpActionResult DeleteUser(SsoPayload ssoPayload)
        {
            //UserManager umManager = new UserManager();
            //umManager.DeleteUserAccount(umManager.FindUserById(id));
            return Ok();
        }

        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        [HttpGet]
        [ActionName("UserProfile")]
        public IHttpActionResult GetUserProfile([FromUri] int accountId)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
            );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanReadAStudentPublicInformation"
            };

            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                try
                {
                    UserManager userManager = new UserManager();
                    string updatedToken = sm.RefreshSession(securityContext.Token);
                    var results = userManager.GetUserProfile(accountId);
                    return Ok(new { User = results, SITtoken = updatedToken });
                }
                catch (Exception x) when (x is ArgumentException)
                {
                    return Content(HttpStatusCode.BadRequest, x.Message);
                }
                catch (Exception x)
                {
                    return Content(HttpStatusCode.InternalServerError, x.Message);
                }
            }
        }


    }
}
