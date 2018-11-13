using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.DataAccess;

namespace Authorization
{
    public class AuthorizedUser
    {
        public AuthorizedUser()
        {
           

        }
        public AuthorizedUser(String _userName, String _userType)
        {
            UserName = _userName;
            UserType = _userType;

        }

        public String UserName { get; set; }
        public String UserType { get; set; }

        //List of claims
        public List<Claim> UserClaims { get; set; }

        //Check if the user has a claim 
        public bool hasClaim(String claimName)
        {
            foreach(Claim claim in UserClaims)
            {
                if(claim.ClaimType.Equals(claimName) && claim.ClaimValue == true)
                {
                    return true;
                }

            }
            return false;
        }






    }




}
