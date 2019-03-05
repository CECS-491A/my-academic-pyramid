using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace SecurityLayer.Authorization
{
    class ClaimChecker : IChecker
    {
        private IChecker _nextChecker;
        private List<Claim> _requiredClaims;

        public ClaimChecker(List<Claim> requiredClaims)
        {
            _requiredClaims = requiredClaims;
        }

        /// <summary>
        /// Validates the claims of a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true if the user passess the authorization check and the
        /// rest in the chain.
        /// Otherwise, false.</returns>
        public bool Process(User user)
        {
            if (_checkClaim(user))
            {
                if (_nextChecker != null)
                {
                    return _nextChecker.Process(user);
                }
                return true;
            }
            return false;
        }

        public void SetNextChecker(IChecker checker)
        {
            _nextChecker = checker;
        }

        /// <summary>
        /// checks that user has the required claim in the requiredClaims. It would throw the exception, if the requireClaims is null.
        /// If the required claim is in the requiredClaims, it would return true and user would be able to use the feature that user requested to use.
        /// If the required claim is not in the requiredClaims, it would return false and user wouldn't be able to use the feature.
        /// </summary>
        /// <param name="requiredClaims"></param> required claim(s) to get a permission to use the feature
        /// <returns> true/false </returns>
        private bool _checkClaim(User user)
        {
            // Checks if each required claim exists in user's claim list
            return _requiredClaims.All(rc =>
           {
               // body of lambda function
               // looks for a uc (user claim) that matches rc (required claim)
               Claim foundClaim = user.Claims.ToList().Find(
                   uc => uc.Value.Equals(rc.Value)
               );
               // If claim not found, then foundClaim will be null.
               return (foundClaim != null);
           });
        }
    }
}
