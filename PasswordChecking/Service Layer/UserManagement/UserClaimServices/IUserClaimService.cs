using System;
using System.Collections.Generic;


namespace DataAccessLayer.UserManagement.UserClaimServices
{
    public interface IUserClaimService
    {
        

        IList<String> GetClaims(User user);
        void AddClaim(User user, String claim );
        void RemoveClaim(User user, String claim);
        
        
    }
}
