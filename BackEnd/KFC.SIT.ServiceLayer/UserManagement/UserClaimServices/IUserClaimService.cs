using System;
using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.Models;


namespace ServiceLayer.UserManagement.UserClaimServices
{

    /// <summary>
    ///  Interface for UserClaimServices 
    /// </summary>
    public interface IUserClaimServices
    {
        
        Account AddClaim(Account user, Claim claim );
        Account RemoveClaim(Account user, string claimStr);
        
    }
}
