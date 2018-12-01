using Authorization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Authorization
{
    public class AuthorizationManager:IAuthorizationManager
    {
        
        private CustomUser authorizedUser;

        public AuthorizationManager(CustomUser user)
        { 
            authorizedUser = user;
        }

        public bool CheckClaims(List<string> requiredClaimTypes)
        {
            throw new NotImplementedException();
        }



        //public bool CheckClaims(List<String> requiredClaimTypes)
        //{

        //    // To check if a claim type exists in user's claim list and the claim value is true
        //    if (requiredClaimTypes.All(rc =>
        //    {
        //        // body of lambda function
        //        Claim foundClaim = authorizedUser.userClaims.Find(uc => uc.ClaimType.Equals(rc));
        //        if (foundClaim != null && foundClaim.ClaimValue == true)
        //            return true;
        //        else
        //            return false;

        //    }))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
