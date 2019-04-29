using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;
using System;
//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DataAccessLayer.Models
{
    class TokenManager
    {

        //private static string Secret = "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==";
        //public static string GenerateToken(string username)
        //  {
        //      byte[] key = Convert.FromBase64String(Secret);
        //      SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
        //      SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        //      {
        //          Subject = new ClaimsIdentity(new[] {
        //              new Claim(ClaimTypes.Name, username)}),
        //          Expires = DateTime.UtcNow.AddMinutes(30),
        //          SigningCredentials = new SigningCredentials(securityKey,
        //          SecurityAlgorithms.HmacSha256Signature)
        //      };
  	     
  	     //JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        //      JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
        //      return handler.WriteToken(token);
        //  }
    }
}
