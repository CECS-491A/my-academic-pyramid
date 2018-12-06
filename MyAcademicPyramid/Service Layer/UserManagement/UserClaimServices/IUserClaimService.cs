using System;
using System.Collections.Generic;


namespace DataAccessLayer.UserManagement.UserClaimServices
{
    public interface IUserClaimService
    {
        
        void AddClaim(User user, Claim claim );
        void RemoveClaim(User user, Claim claim);
        
    }
}
