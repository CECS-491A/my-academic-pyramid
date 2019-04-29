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
        private const string SHARED_SSO_SECRET
            = "D078F2AFC7E59885F3B6D5196CE9DB716ED459467182A19E04B6261BBC8E36EE";
        public static bool ValidateSSOPayload(SSOPayload payload)
        {
            string payloadStr = $"ssoUserId={payload.SSOUserId};" +
                                $"email={payload.Email};" +
                                $"timestamp={payload.Timestamp};";
            byte[] keyBytes = Encoding.UTF8.GetBytes(SHARED_SSO_SECRET);
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