using DataAccessLayer;
using System;
using System.Collections.Generic;

namespace ManagerLayer.Logic.Authorization.AuthorizationManagers
{
    /// <summary>
    /// Interface that forces the class implementing CheckClaims(List<String> requiredClaimTypes).
    /// Makes the code extensible
    /// </summary>
    interface IAuthorizationManager
    {
        bool CheckClaims(List<Claim> requiredClaimTypes);
    }
}