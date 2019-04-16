using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace SecurityLayer.Authorization
{
    public class SecurityContext
    {
        public String UserName { get; set; }
        public List<Claim> Claims { get; set; }
        public SecurityContext(Dictionary<string, string> payload)
        {
            string userName = null;
            if (payload.TryGetValue("username", out userName))
            {
                UserName = payload["username"];
            }
            else
            {
                throw new ArgumentException("Payload has not username entry.");
            }
        }

        
    }
}
