using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Authorization
{
    public abstract class AuthorizationController
    {
        
        User authorizedUser;

        public AuthorizationController(User user)
        { 
            authorizedUser = user;
        }



        public bool checkClaim(String claimType)
        {
            // To check if a claim type exists in user's claim list and the claim value is true
            if (authorizedUser.userClaims.Find(c => c.ClaimType.Equals(claimType) && c.ClaimValue == true) != null)
            {
                return true;
            }
            return false;
        }
    }
}
