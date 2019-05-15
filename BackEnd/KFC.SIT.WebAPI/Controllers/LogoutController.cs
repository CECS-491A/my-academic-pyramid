using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SecurityLayer;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using WebAPI.Gateways.UserManagement;
using SecurityLayer.Sessions;
using SecurityLayer.Authorization;
using KFC.SIT.WebAPI.Utility;

namespace KFC.SIT.WebAPI.Controllers
{
    public class LogoutController : ApiController
    {
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]int userId)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(Request.Headers);
            if (securityContext == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            DatabaseContext db = new DatabaseContext();
            SessionManager sm = new SessionManager();
            // TODO modify this to allow admins to use this.
            if (securityContext.UserId != userId)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            if (!sm.ValidateSession(securityContext.Token)) {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            try
            {
                sm.InvalidateSession(securityContext.Token);
                //

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed);
            }
        }
    }
}