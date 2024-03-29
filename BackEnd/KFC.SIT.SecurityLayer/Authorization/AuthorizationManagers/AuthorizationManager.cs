﻿
using DataAccessLayer;
using DataAccessLayer.Models;
using SecurityLayer.Sessions;
using ServiceLayer.UserManagement.UserAccountServices;
using System;
using System.Collections.Generic;
using System.Linq;



namespace SecurityLayer.Authorization.AuthorizationManagers
{
    /// <summary>
    /// Checks the existence of the user and privileges of the user.  
    /// An instance of AuthorizationManager will be created for every request from the user by Controller.
    /// </summary>
    public class AuthorizationManager
    {

        SecurityContext context;

        

        /// <summary>
        /// Constructor of AuthorizationManager.
        /// Takes in a SecurityContext as a parameter and save the value.
        /// It would throw an exception, if the context is null
        /// </summary>
        /// <param name="context"></param>
        public AuthorizationManager(SecurityContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("SecurityContext", "context cannot be null.");
            }

            this.context = context;
        }

        public AuthorizationManager(string token)
        {
            JWTokenManager jwtManager = new JWTokenManager();
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }
            else if (!jwtManager.ValidateSignature(token))
            {
                throw new ArgumentException("token", "Not a valid JSON Web Token.");
            }
            this.context = new SecurityContext(token);
        }

        /// <summary>
        /// checks that user has the required claim in the requiredClaims. It would throw the exception, if the requireClaims is null.
        /// If the required claim is in the requiredClaims, it would return true and user would be able to use the feature that user requested to use.
        /// If the required claim is not in the requiredClaims, it would return false and user wouldn't be able to use the feature.
        /// </summary>
        /// <param name="requiredClaims"></param> required claim(s) to get a permission to use the feature
        /// <returns> true/false </returns>
        public bool CheckClaims(List<string> requiredClaims)
        {
            if (requiredClaims == null)
            {
                throw new ArgumentNullException(
                    "requiredClaims", "List of required claims can't be null."
                );
            }

            

            // Checks if each required claim exists in user's claim list
            return requiredClaims.All(rc =>
            {
                // body of lambda function
                // looks for a uc (user claim) that matches rc (required claim)
                string foundClaim = context.Claims.ToList().Find(
                    uc => uc.Equals(rc)
                );
                // If claim not found, then foundClaim will be null.
                return (foundClaim != null);
            });
        }

        /// <summary>
        /// Checks if the user who made the request is at a higher level than the targeted user 
        /// </summary>
        /// <param name="callingUser"></param> can't be null or it will throw an exception.
        /// <param name="targetedUser"></param> can't be null or it will throw an exception.
        /// <returns> true/false </returns>
        public bool HasHigherPrivilege(Account callingUser, Account targetedUser)
        {
            if (callingUser == null)
            {
                throw new ArgumentNullException("callingUser", "Parameter can't be null.");
            }
            else if (targetedUser == null)
            {
                throw new ArgumentNullException("targetedUser", "Parameter can't be null.");
            }
            else
            {
                if (callingUser.Id == targetedUser.Id)
                {
                    return true;
                }

                else if (FindHeight(callingUser) < FindHeight(targetedUser))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        } // end of HasHigherPrivilege()

        /// <summary>
        /// Finds level of user by trarvese back to the root using referenced parent Id
        /// </summary>
        /// <param name="user"></param> can't be null or it will throw an exception
        /// <returns> level </returns>
        public int FindHeight(Account user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user", "user can't be null.");
            }

            UserManagementServices uMS = new UserManagementServices(new DatabaseContext());
            int level = 0;
            while (user.ParentUser_Id != null)
            {
                int parentId = (int)user.ParentUser_Id;
                user = uMS.FindById(parentId);
                level += 1;
            }

            return level;
        }

    }
}

