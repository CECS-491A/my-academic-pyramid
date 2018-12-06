using System;
using System.Collections.Generic;
using DataAccessLayer;


namespace ServiceLayer.UserManagement.UserClaimServices
{
    public interface IUserClaimService
    {
        
        void AddClaim(User user, Claim claim );
        void RemoveClaim(User user, Claim claim);
        
    }
}
