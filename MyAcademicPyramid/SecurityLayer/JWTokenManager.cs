using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Web;

namespace SecurityLayer
{
    public static class JWTokenManager
    {
        public static string GenerateToken(Dictionary<string, string> payload, 
                                           string algorithm)
        {
            string token = "";
            string tokenType = "JWT";
            // TODO: get key from flat file, so make key a paramater of function.
            string key = "JOIENFUPBFJESFOIEJGNEOPFENPPFENFBPPNFIEPUBRGHFCMK";
            // Created header
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("alg", algorithm);
            header.Add("typ", tokenType);
            string headerEncode = UrlBase64DictEncoding(header);
            string payloadEncode = UrlBase64DictEncoding(payload);
            string signEncode = GenerateTokenSignature(headerEncode, payloadEncode, key);
            

            Console.Out.WriteLine($"Header: {headerEncode} Payload: {payloadEncode}\n");
            token = $"{headerEncode}.{payloadEncode}.{signEncode}";

            return token;
        }

        public static bool validateToken(string token)
        {
            bool isValidated = false;
            string key = "JOIENFUPBFJESFOIEJGNEOPFENPPFENFBPPNFIEPUBRGHFCMK";
            string[] tokenParts = token.Split('.');
            if (tokenParts.Length != 3)
            {
                return isValidated;
            }
            string encodedHeader = tokenParts[0];
            string encodedPayload = tokenParts[1];
            string signToCheck = tokenParts[2];
            string encodedSign = GenerateTokenSignature(
                encodedHeader, encodedPayload, key
            );
            Console.Out.WriteLine($"Encoding of newly created sign: {encodedSign}");
            isValidated = signToCheck.Equals(encodedSign);

            return isValidated;
        }

        private static Dictionary<string, string> GetPayload(string encodedToken)
        {
            //TODO finish this.
        }

        private static string UrlBase64DictEncoding(Dictionary<string, string> dict)
        {
            string jsonDict = JsonConvert.SerializeObject(dict);
            byte[] byteJsonDict = Encoding.UTF8.GetBytes(jsonDict);
            string base64EncodedDict = HttpServerUtility.UrlTokenEncode(byteJsonDict);
            return base64EncodedDict;
        }

        private static string GenerateTokenSignature(string encodedHeader, 
                                                     string encodedPayload,
                                                     string key)
        {
            byte[] preSign = Encoding.UTF8.GetBytes(encodedHeader + encodedPayload);
            byte[] byteKey = Encoding.UTF8.GetBytes(key);
            HMACSHA256 hmac = new HMACSHA256(byteKey);
            byte[] signature = hmac.ComputeHash(preSign);
            return HttpServerUtility.UrlTokenEncode(signature);
        }

        public static string GenerateToken(String jsonPayload, string algorithm)
        {
            string token = "";

            return token;
        }
    }
}
