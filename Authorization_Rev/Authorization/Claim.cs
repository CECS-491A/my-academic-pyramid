using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class Claim
    {
        public Claim(String _claimType, bool _claimValue)
        {
            ClaimType = _claimType;
            ClaimValue = _claimValue;
        }

        public String ClaimType { get; set; }
        public bool ClaimValue { get; set; }
    }
}
