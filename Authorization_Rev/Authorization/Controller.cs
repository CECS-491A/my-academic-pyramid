using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    //This class is used to authorize user's actions by compare user's claim to required claim.
    public class Controller : IAdminAction, IStudentAction
    {
        // Method for Admin to add account
        private User authorizedUser;
        public Controller(User _user)
        {
            authorizedUser = _user;
        }

        // Method for admin to add user ccount
        public void AddUserAccount()
        {
            // Check if user has require claim
            if (checkClaim("CanAddUserAccount"))
            {

                Console.WriteLine($"User {authorizedUser.UserName} can add user account");

            }

            else
            {
                Console.WriteLine($"User {authorizedUser.UserName} does not have permission to add user account.");
            }

        }

        // Method for admin delete other account
        public  void DeleteOtherAccount(String userid )
        {
            // Check if user has require claim
            if (checkClaim("CanDeleteOtherAccount"))
            {
                Console.WriteLine($"User {authorizedUser.UserName} can delete other account" );
            }
            else
            {
                Console.WriteLine($"User {authorizedUser.UserName} does not have permission to remove user account.");
            }
        }

        // Method for user delete their own account
        public void DeleteUserOwnAccount()
        {
            if (checkClaim("CanDeleteUserOwnAccount"))
            {
                Console.WriteLine($"User {authorizedUser.UserName} can delete other account");
            }
            else
            {
                Console.WriteLine($"User {authorizedUser.UserName} does not have permission to remove user account.");
            }
        }

        // Check claim method used to authorize an action.
       private bool checkClaim(String claimType)
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
