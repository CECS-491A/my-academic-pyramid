using System;
using System.Collections.Generic;
using Authorization;
using Authorization.Interfaces;


namespace UserManagement.Interfaces
{
    public interface IUserClaimService
    {
        

        IList<String> GetClaims(User user);
        void AddClaim(User user, String claim );
        void RemoveClaim(User user, String claim);
        
        
    }
}
