using System;
using System.Collections.Generic;
using System.Text;
using Authorization.Interfaces;


namespace UserManagement.Interfaces
{
    public interface IUserClaimService<CustomUser>: IUserAccountService<CustomUser> where CustomUser : class, IUser
    {
        

        //Claim claim = new Claim(_claimType, _claimValue);
        //userClaims.Add(claim);

        IList<String> getClaims(CustomUser user);
        void AddClaim(CustomUser user, String claim );
        void RemoveClaim(CustomUser user, String claim);
        
        
    }
}
