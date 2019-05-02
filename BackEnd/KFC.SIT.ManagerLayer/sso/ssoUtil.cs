using ServiceLayer.UserManagement.UserAccountServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ManagerLayer.sso
{
    public static class ssoUtil
    {
        public const string SHARED_SECRET
                = "8B7BF974D6A5377020468EB6A336FFC98B465F158F089F89810435885D344E98";
        public static bool ValidateSSOPayload(SsoPayload payload)
        {
            
            string payloadStr = $"ssoUserId={payload.SSOUserId};" +
                                    $"email={payload.Email};" +
                                    $"timestamp={payload.Timestamp};";
            byte[] keyBytes = Encoding.UTF8.GetBytes(SHARED_SECRET);
            int length = SHARED_SECRET.Length;
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payloadStr);
            HMACSHA256 hmac = new HMACSHA256(keyBytes);

            byte[] calculatedSignatureBytes = hmac.ComputeHash(payloadBytes);
            string calculatedSignatureStr = System.Convert.ToBase64String(
                calculatedSignatureBytes
            );
            return calculatedSignatureStr.Equals(payload.Signature);
        }

    }
}