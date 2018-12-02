using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Authorization
{
    public class User
    {
        //List of string to store user Claims
        public List<string> userClaims;

        public User()
        {
 
        }
             
        public User(String _userName, String _userType)
        {
            UserName = _userName;
            UserType = _userType;
            userClaims = new List<string>();

        }

        public String UserName { get; private set; }
        public String UserType { get; private set; }

        // Add claim method
        public void addClaim(String claim)
        {
            userClaims.Add(claim);
        }

    }




}
