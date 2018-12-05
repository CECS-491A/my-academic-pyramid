using System;
using System.Collections.Generic;

namespace ManagerLayer.Logic.Authorization.AuthorizationManagers
{
    interface IAuthorizationManager
    {
        bool CheckClaims(List<String> requiredClaimTypes);
    }
}