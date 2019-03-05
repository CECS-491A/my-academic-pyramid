
using DataAccessLayer;
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
    public class AuthorizationManager:IAuthorizationManager
    {
        
        private User authorizedUser;
        private AuthorizationChain authorizationChain;
        /// <summary>
        /// Default constructor
        /// </summary>
        public AuthorizationManager()
        {

        }

        /// <summary>
        /// Constructor of AuthorizationManager.
        /// Takes in user and an already built chain as parameters.
        /// It would throw an exception, if the user or passedChain is null.
        /// </summary>
        /// <param name="user"></param>
        public AuthorizationManager(User user, AuthorizationChain passedChain)
        { 
            if (user == null || passedChain == null)
            {
                throw new ArgumentNullException("user", "User cannot be null.");
            }
                
            authorizedUser = user;
            authorizationChain = passedChain;
        }


        /// <summary>
        /// Checks if the user who made the request is at a higher level than the targeted user 
        /// </summary>
        /// <param name="callingUser"></param> can't be null or it will throw an exception.
        /// <param name="targetedUser"></param> can't be null or it will throw an exception.
        /// <returns> true/false </returns>
        public bool HasHigherPrivilege(User callingUser, User targetedUser)
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

                else if(FindHeight(callingUser) < FindHeight(targetedUser))
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
        public int FindHeight(User user)
        {
            if(user == null)
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

