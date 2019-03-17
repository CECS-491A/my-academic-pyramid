using System;
using System.Collections.Generic;
using DataAccessLayer;


namespace ServiceLayer.UserManagement.UserClaimServices
{

    /// <summary>
    ///  Interface for UserClaimServices 
    /// </summary>
    public interface IUserClaimServices
    {
        
        User AddClaim(User user, Claim claim );
        User RemoveClaim(User user, Claim claim);
        
    }
}
