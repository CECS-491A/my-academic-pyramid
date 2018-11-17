using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Authorization
{
    public class User
    {
        //List of type Claim to store user Claims
        public List<Claim> userClaims;

        public User()
        {
 
        }
             
        public User(String _userName, String _userType)
        {
            
            UserName = _userName;
            UserType = _userType;
            userClaims = new List<Claim>();

        }

        public String UserName { get; set; }
        public String UserType { get; set; }

        // Add claim method
        public void addClaim(String _claimType, bool _claimValue)
        {
            Claim claim = new Claim(_claimType, _claimValue);
            userClaims.Add(claim);
        }

    }




}
