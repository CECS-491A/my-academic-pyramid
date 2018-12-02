using System;
using System.Collections.Generic;
using Authorization.Interfaces;


namespace UserManagement.Interfaces
{
    public interface IUserClaimService<CustomUser> where CustomUser : class, IUser
    {
        

        //Claim claim = new Claim(_claimType, _claimValue);
        //userClaims.Add(claim);

        IList<String> GetClaims(CustomUser user);
        void AddClaim(CustomUser user, String claim );
        void RemoveClaim(CustomUser user, String claim);
        
        
    }
}
