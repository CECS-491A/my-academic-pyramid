using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Newtonsoft.Json;
using SecurityLayer.Sessions;

namespace SecurityLayer.Authorization
{
    public class SecurityContext
    {
        public string UserName { get; set; }
        public List<string> Claims { get; set; }
        public string Token { get; set; }
        
        public SecurityContext(string token)
        {
            JWTokenManager tokenManager = new JWTokenManager();
            Dictionary<string, string> payload = tokenManager.DecodePayload(token);
            Initialize(token, payload);
        }
        public SecurityContext(string token, Dictionary<string, string> payload)
        {
            Initialize(token, payload);
        }

        private void Initialize(string token, Dictionary<string, string> payload)
        {
            Token = token;
            string userName = null;
            string claimsStr = null;
            if (payload.TryGetValue("username", out userName))
            {
                UserName = userName;
            }
            else
            {
                throw new ArgumentException("Payload has no username entry.");
            }
            if (payload.TryGetValue("claims", out claimsStr))
            {
                Claims = JsonConvert.DeserializeObject<List<string>>(claimsStr);
            }
            else
            {
                throw new ArgumentException("Payload has no claims entry.");
            }
        }
        
    }
}
