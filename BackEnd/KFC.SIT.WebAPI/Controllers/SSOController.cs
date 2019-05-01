using SecurityLayer.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ManagerLayer.sso;
using WebAPI.UserManagement;
using DataAccessLayer.DTOs;

namespace KFC.SIT.WebAPI.Controllers
{
    public class SSOController : ApiController
    {
        [HttpPost]
        [ActionName("Logout")]
        public IHttpActionResult Logout(SsoPayload payload)
        {
            if(!SignatureService.IsValidClientRequest(
                    payload.SSOUserId, payload.Email, long.Parse(payload.Timestamp),
                    payload.Signature
                ))
            {
                return Unauthorized();
            }
            // Find userid using sso id
            UserManager userManager = new UserManager();
            SessionManager sm = new SessionManager();
            SsoManager ssoManager = new SsoManager();
            UserDTO userDto = ssoManager.FindUserById(new Guid(payload.SSOUserId));
            if (userDto == null)
            {
                return NotFound();
            }

            string token = sm.GetSessionToken(userDto.Id);
            if (token == null)
            {
                return Ok();
            }
            sm.InvalidateSession(token);

            return Ok();
        }

        [HttpPost]
        [ActionName("DeleteUser")]
        public IHttpActionResult DeleteUser(SsoPayload payload)
        {
            if (!SignatureService.IsValidClientRequest(
                    payload.SSOUserId, payload.Email, long.Parse(payload.Timestamp),
                    payload.Signature
                ))
            {
                return Unauthorized();
            }
            if (payload.SSOUserId == null)
            {
                return BadRequest("No SSO user id passed.");
            }
            // Find userid using sso id
            UserManager userManager = new UserManager();
            SessionManager sm = new SessionManager();
            SsoManager ssoManager = new SsoManager();
            ssoManager.DeleteUserBySsoId(new Guid(payload.SSOUserId));

            return Ok();
        }

        [HttpOptions]
        [ActionName("HealthCheck")]
        public IHttpActionResult HealthCheck()
        {
            return Ok();
        }
    }
}