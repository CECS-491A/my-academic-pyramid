using DataAccessLayer;
using DataAccessLayer.DTOs;
using KFC.SIT.WebAPI.Utility;
using SecurityLayer.Authorization;
using SecurityLayer.Authorization.AuthorizationManagers;
using SecurityLayer.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Gateways.UserManagement;
using static ServiceLayer.ServiceExceptions.UserManagementExceptions;

namespace KFC.SIT.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        [HttpPost]
        [ActionName("EditUser")]
        public IHttpActionResult EditUserInfo(UserProfileDTO newProfileInfo)
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

            List<string> requiredClaims = new List<string>()
            {
                "CanEditOwnAccount"
            };
            UserManager um = new UserManager();

            try
            {
                um.EditStudentProfile(securityContext.UserId, newProfileInfo);
                string updatedToken = sm.RefreshSession(securityContext.Token);
                return Ok(updatedToken);
            }
            catch (Exception ex) when (ex is AccountNotFoundException)
            {
                return BadRequest("Account not found");
            }
            catch(Exception ex) when (ex is NotAStudentException)
            {
                return BadRequest("Can't modify a non student account.");
            }
            catch(Exception ex) when (ex is DepartmentNotFoundException)
            {
                return BadRequest("Department doesn't exist.");
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}