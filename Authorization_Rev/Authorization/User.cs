using Authorization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class User 
    {
        List<String> claims;
        public User(String _userName)
        {
            UserName = _userName;
            claims = new List<string>();

        }

        public User(String _userName, String _role)
        {
            UserName = _userName;
            claims = new List<string>();

        }
        public String UserName { get; set; }
        public List<string> userClaims
        {
            get
            {
                return claims;
            }
            set
            {
                claims = value;
            }

        }

        
    }
}
