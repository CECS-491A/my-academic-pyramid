using SecurityLayer.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ManagerLayer.sso;
using WebAPI.UserManagement;

namespace KFC.SIT.WebAPI.Controllers
{
    public class SSOController : ApiController
    {
        [HttpPost]
        [ActionName("Logout")]
        public IHttpActionResult Logout(SSOPayload payload)
        {
            if(!ssoUtil.ValidateSSOPayload(payload))
            {
                return Unauthorized();
            }
            // Find userid using sso id
            UserManager userManager = new UserManager();
            SessionManager sm = new SessionManager();
            // TODO finish this.
            //sm.InvalidateSession();

            return Ok();
        }
    }
}