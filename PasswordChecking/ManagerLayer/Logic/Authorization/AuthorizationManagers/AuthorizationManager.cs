﻿
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;



namespace ManagerLayer.Logic.Authorization.AuthorizationManagers
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizationManager:IAuthorizationManager
    {
        
        private User authorizedUser;

        public AuthorizationManager(User user)
        { 
            if (user == null)
                throw new ArgumentNullException("user", "User cannot be null.");
            authorizedUser = user;
        }


        /// <summary>
        /// checks that user has the required claim in the requiredClaims. It would throw the exception, if the requireClaims is null.
        /// If the required claim is in the requiredClaims, it would return true and user would be able to use the feature that user requested to use.
        /// If the required claim is not in the requiredClaims, it would return false and user wouldn't be able to use the feature.
        /// </summary>
        /// <param name="requiredClaims"></param>
        /// <returns></returns>
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
                string foundClaim = authorizedUser.Claims.Find(
                    uc => uc.Equals(rc)
                );
                // If claim not found, then foundClaim will be null.
                return (foundClaim != null);
            });
        }
    }
}