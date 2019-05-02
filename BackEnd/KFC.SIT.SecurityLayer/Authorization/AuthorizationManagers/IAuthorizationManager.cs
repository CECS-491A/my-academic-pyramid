using DataAccessLayer.Models;
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
        bool HasHigherPrivilege(Account callingUser, Account targetedUser);
        int  FindHeight(Account user);

    }
}