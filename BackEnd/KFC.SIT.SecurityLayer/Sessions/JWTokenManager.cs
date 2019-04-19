using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Web;
using DataAccessLayer;
using SecurityLayer.utility;

namespace SecurityLayer.Sessions
{
    public class JWTokenManager
    {
        private static string key = "JOIENFUPBFJESFOIEJGNEOPFENPPFENFBPPNFIEPUBRGHFCMK";
        private UrlSafeBase64Encoder _encoder;
        public const string INITIAL_TIME_KEY = "iat";
        public const string REFRESH_TIME_KEY = "ref";
        public const string EXPIRATION_TIME_KEY = "exp";

        public JWTokenManager()
        {
            _encoder = new UrlSafeBase64Encoder();
        }

        /*TODO:
         * Seperate remove expired tokens process.
         * Random string for security
         * Headers for cross scripts.
         * Action to get token, Validate, Refresh
         * UI.
         */

        public Dictionary<string, string> DecodePayload(string encodedToken)
        {
            //TODO finish this.
            string[] tokenParts = encodedToken.Split('.');
            if (tokenParts.Length != 3)
            {
                throw new ArgumentException("Invalid JWToken.");
            }
            string encodedPayload = tokenParts[1];
            return _encoder.Decode(encodedPayload);
        }       

        public bool ValidateSignature(string token)
        {
            bool isValidated = false;
            string[] tokenParts = token.Split('.');
            if (tokenParts.Length != 3)
            {
                // Token is not in correct format.
                return isValidated;
            }
            string encodedHeader = tokenParts[0];
            string encodedPayload = tokenParts[1];
            string signToCheck = tokenParts[2];
            string encodedSign = GenerateTokenSignature(
                encodedHeader, encodedPayload, key
            );
            isValidated = signToCheck.Equals(encodedSign);

            return isValidated;
        }

        public static string GenerateToken(String jsonPayload, string algorithm)
        {
            string token = "";

            return token;
        }

        public string CreateToken(Dictionary<string, string> payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException("Payload is null");
            }
            string token = "";
            string tokenType = "JWT";
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("alg", "SHA256");
            header.Add("typ", tokenType);
            string headerEncode = _encoder.Encode(header);
            string payloadEncode = _encoder.Encode(payload);
            string signEncode = GenerateTokenSignature(headerEncode, payloadEncode, key);
            Console.Out.WriteLine($"Header: {headerEncode} Payload: {payloadEncode}\n");
            token = $"{headerEncode}.{payloadEncode}.{signEncode}";
            return token;
        }

        private string GenerateTokenSignature(string encodedHeader,
                                                     string encodedPayload,
                                                     string key)
        {
            if (encodedHeader == null || encodedPayload == null 
                    || key == null)
            {
                throw new ArgumentNullException("Null argument passed.");
            }
            byte[] preSign = Encoding.UTF8.GetBytes(encodedHeader + "." + encodedPayload);
            byte[] byteKey = Encoding.UTF8.GetBytes(key);
            HMACSHA256 hmac = new HMACSHA256(byteKey);
            byte[] signature = hmac.ComputeHash(preSign);
            hmac.Dispose();
            return _encoder.ToUrlSafeBase64Str(signature);
        }

        public bool VerifyHeader(string jwtoken)
        {
            if (jwtoken == null)
            {
                return false;
            }
            string tokenTypeKeyStr = "typ";
            string algKeyStr = "alg";
            string validTokenType = "JWT";
            string validAlgorithm = "SHA256";
            string headerStr = jwtoken.Split('.')[0];
            Dictionary<string, string> headerDict
                = _encoder.Decode(headerStr);

            if (headerDict == null)
            {
                return false;
            }
            if (!(headerDict.ContainsKey(tokenTypeKeyStr)
                && headerDict.ContainsKey(algKeyStr)))
            {
                return false;
            }
            return (headerDict[tokenTypeKeyStr] == validTokenType
                    && headerDict[algKeyStr] == validAlgorithm);
        }
    }
}
