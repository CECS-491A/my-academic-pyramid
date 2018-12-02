using System;
using System.Collections.Generic;


namespace ServiceLayer.UserManagement.UserClaimServices
{
    public interface IUserClaimService
    {
        

        IList<String> GetClaims(User user);
        void AddClaim(User user, String claim );
        void RemoveClaim(User user, String claim);
        
        
    }
}
