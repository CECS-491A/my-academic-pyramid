using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Newtonsoft.Json;

namespace SecurityLayer.Authorization
{
    public class SecurityContext
    {
        public string UserName { get; set; }
        public List<string> Claims { get; set; }
        public SecurityContext(Dictionary<string, string> payload)
        {
            string userName = null;
            string claimsStr = null;
            if (payload.TryGetValue("username", out userName))
            {
                UserName = userName;
            }
            else
            {
                throw new ArgumentException("Payload has not username entry.");
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
