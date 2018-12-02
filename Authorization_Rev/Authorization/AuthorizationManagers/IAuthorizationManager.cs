using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Interfaces
{
    interface IAuthorizationManager
    {
        bool CheckClaims(List<String> requiredClaimTypes);
    }
}