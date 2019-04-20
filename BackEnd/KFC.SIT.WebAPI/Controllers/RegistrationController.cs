using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SecurityLayer.Sessions;
using SecurityLayer;
using SecurityLayer.Authorization.AuthorizationManagers;
using Newtonsoft.Json;
using SecurityLayer.Authorization;
using DataAccessLayer;
using WebAPI.UserManagement;
using System.Web.Http.Cors;
using DataAccessLayer.Models;

namespace KFC.SIT.WebAPI.Controllers.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RegistrationController : ApiController
    {

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post(RegistrationData registrationData)
        {
            string token;
            if (!Request.Headers.Contains("Authorization"))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            try
            {
                // TODO add code checking for this.
                string[] parts = Request.Headers.GetValues("Authorization").First().Split(' ');
                if (parts.Length !=2 || parts[0] != "Bearer")
                {
                    return Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
                token = parts[1];
            }
            catch(InvalidOperationException) // Catch when Token header has no value.
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            SessionManager sm = new SessionManager();
            JWTokenManager jwtManager = new JWTokenManager();
            if (!sm.ValidateSession(token))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            Dictionary<string, string> payload = jwtManager.DecodePayload(token);
            // TODO make payload the context. Make sure the authorization chain
            // TODO doesn't require more than the payload context contains.
            SecurityContext securityContext = new SecurityContext(payload);
            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanRegister"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                UserManager um = new UserManager();
                User user = um.FindByUserName(securityContext.UserName);
                if (user == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User doesn't exist");
                }
                user.FirstName = registrationData.FirstName;
                user.LastName = registrationData.LastName;
                // TODO test this.
                user.DateOfBirth = registrationData.DateOfBirth;
                user.Category = new Category("Student");
                um.UpdateUserAccount(user);
                um.RemoveClaimAction(user.Id, "CanRegister");
                um.AutomaticClaimAssigning(user);
                string updatedToken = sm.RefreshSession(token, payload);
                Dictionary<string, string> responseContent = new Dictionary<string, string>()
                {
                    { "SITtoken", updatedToken}
                };
                return Request.CreateResponse(HttpStatusCode.OK, responseContent);
            }
        }

    }
}