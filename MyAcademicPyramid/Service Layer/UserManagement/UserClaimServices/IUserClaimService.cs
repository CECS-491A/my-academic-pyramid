using System;
using System.Collections.Generic;
using DataAccessLayer.Models;
using DataAccessLayer;


namespace ServiceLayer.UserManagement.UserClaimServices
{

    /// <summary>
    ///  Interface for UserClaimServices 
    /// </summary>
    public interface IUserClaimServices
    {
        
        void AddClaim(User user, Claim claim );
        void RemoveClaim(User user, Claim claim);
        
    }
}
