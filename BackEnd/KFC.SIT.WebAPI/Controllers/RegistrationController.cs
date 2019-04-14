using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SecurityLayer.Sessions;
using SecurityLayer.Authorization.AuthorizationManagers;
using Newtonsoft.Json;

namespace KFC.SIT.WebAPI.Controllers
{
    public class RegistrationController : ApiController
    {

        // POST api/<controller>
        public IHttpActionResult Post(RegistrationData registrationData)
        {
            string token;
            if (!Request.Headers.Contains("Token"))
            {
                return Unauthorized();
            }
            try
            {
                token = Request.Headers.GetValues("Token").First();
            }
            catch(InvalidOperationException) // Catch when Token header has no value.
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            JWTokenManager jwtManager = new JWTokenManager();
            if (!sm.ValidateSession(token))
            {
                return Unauthorized();
            }
            Dictionary<string, string> payload = jwtManager.DecodePayload(token);
            // TODO make payload the context. Make sure the authorization chain
            // TODO doesn't require more than the payload context contains.
            var claims = JsonConvert.DeserializeObject(payload["Claims"]);
            AuthorizationManager authorizationManager = new AuthorizationManager();
        }

    }
}