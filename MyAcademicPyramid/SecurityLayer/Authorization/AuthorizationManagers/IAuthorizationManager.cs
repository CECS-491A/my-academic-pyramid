using DataAccessLayer;
using System;
using System.Collections.Generic;

namespace SecurityLayer.Authorization.AuthorizationManagers
{
    /// <summary>
    /// Interface that forces the class implementing CheckClaims(List<String> requiredClaimTypes).
    /// Makes the code extensible
    /// </summary>
    public interface IAuthorizationManager
    {
       
        bool CheckClaims(List<Claim> requiredClaimTypes);
        bool DeletedUserIsChild(User user);
    }
}