using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.sso
{
    public class SsoPayload
    {
        public string Email { get; set; }
        public string Signature { get; set; }
        public string SSOUserId { get; set; }
        public string Timestamp { get; set; }

    }
}
