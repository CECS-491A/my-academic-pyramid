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
        
        private User authorizedUser;

        public AuthorizationManager(User user)
        { 
            if (user == null)
                throw new ArgumentNullException("user", "User cannot be null.");
            authorizedUser = user;
        }



        public bool CheckClaims(List<String> requiredClaims)
        {
            if (requiredClaims == null)
                throw new ArgumentNullException(
                    "requiredClaims", "List of required claims can't be null."
                );
            // Checks if each required claim exists in user's claim list
            return requiredClaims.All(rc =>
            {
                // body of lambda function
                // looks for a uc (user claim) that matches rc (required claim)
                string foundClaim = authorizedUser.userClaims.Find(
                    uc => uc.Equals(rc)
                );
                // If claim not found, then foundClaim will be null.
                return (foundClaim != null);
            });
        }
    }
}
