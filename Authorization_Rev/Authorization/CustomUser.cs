using Authorization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class CustomUser : IUser
    {
        List<String> claims;
        public CustomUser(String _userName)
        {
            UserName = _userName;
            claims = new List<string>();

        }

        public CustomUser(String _userName, String _role)
        {
            UserName = _userName;
            Role = _role;
            claims = new List<string>();

        }
        public String UserName { get; set; }
        public String Role { get; set; }
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
