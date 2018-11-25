using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class Claim
    {
        public Claim(string _claimType, bool _claimValue)
        {
            ClaimType = _claimType;
            ClaimValue = _claimValue;
        }

        public string ClaimType { get; private set; }
        public bool ClaimValue { get; private set; }
    }
}
