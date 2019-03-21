using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Web;
using DataAccessLayer;

namespace SecurityLayer
{
    public class JWTokenManager
    {
        private static string key = "JOIENFUPBFJESFOIEJGNEOPFENPPFENFBPPNFIEPUBRGHFCMK";
        private DatabaseContext _db;
        private SessionServices _sessionServices;

        public JWTokenManager(DatabaseContext db)
        {
            _db = db;
            _sessionServices = new SessionServices(_db);
        }
        
        /*TODO:
         * Seperate remove expired tokens process.
         * Random string for security
         * Headers for cross scripts.
         * Action to get token, Validate, Refresh
         * UI.
         */

        public string GenerateToken(int userid, Dictionary<string, string> payload)
        {
            // TODO: Possibly add a random string to improve security.
            // Link: https://github.com/OWASP/CheatSheetSeries/blob/master/cheatsheets/JSON_Web_Token_Cheat_Sheet_for_Java.md
            string token;
            // TODO: get key from flat file, so make key a paramater of function.
            // Created header
            DateTimeOffset currentTime = DateTimeOffset.UtcNow;
            DateTimeOffset expTime = currentTime.AddMinutes(0.5);
            long currentTimeSec = currentTime.ToUnixTimeSeconds();
            long expTimeSec = expTime.ToUnixTimeSeconds();
            payload.Add("iat", currentTimeSec.ToString());
            payload.Add("refresh", payload["iat"]);
            payload.Add("exp", expTimeSec.ToString());

            token = _createToken(payload);

            UserSession newSession = new UserSession()
            {
                Token = token,
                IsValid = true,
                CreationTime = currentTime,
                RefreshedTime = currentTime,
                ExpirationTime = expTime,
                UserId = userid
            };
            _sessionServices.CreateSession(newSession);

            _db.SaveChanges();

            return token;
        }


        public bool ValidateToken(string token)
        {
            /*
             * Get session entry from database
             * If it doesn't exist, return false.
             * Check if valid. If not, return false.
             * 
             * Validate signature
             * if not valid, return false.
             * 
             * 
             * */
            bool isValid = (ValidateSignature(token) 
                            && !_sessionServices.IsInvalidated(token));
            if (isValid)
            {
                // Check if current time is less than expiredTime
                var payload = GetPayload(token);
                long expireTime;
                if (payload.ContainsKey("exp"))
                {
                    try
                    {
                        expireTime = long.Parse(payload["exp"]);
                        isValid = DateTimeOffset.UtcNow
                                                .ToUnixTimeSeconds() < expireTime;
                    } catch(FormatException)
                    {
                        // log it
                    }
                   
                }
                else
                {
                    isValid = false;
                }

            }
            return isValid;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        public string RefreshToken(string encodedToken, Dictionary<string, string> payload)
        {
            // call SSO token
            DateTimeOffset currentDateTime = DateTimeOffset.UtcNow;
            DateTimeOffset expDateTime = currentDateTime.AddMinutes(30);
            /*
             * Update refresh
             * Update expiration
             * GenerateNewToken
             * UpdateToken
             */
            //TODO add error checking.
            // If payload doesn't have this, then it's not represented by a token.
            payload["refresh"] = currentDateTime.ToUnixTimeSeconds().ToString();
            payload["exp"] = expDateTime.ToUnixTimeMilliseconds()
                                        .ToString();
            string newToken = _createToken(payload);
            _sessionServices.RefreshSession(encodedToken, newToken, 
                                            currentDateTime, expDateTime);
            return newToken;
        }

        public Dictionary<string, string> GetPayload(string encodedToken)
        {
            //TODO finish this.
            string[] tokenParts = encodedToken.Split('.');
            if (tokenParts.Length != 3)
            {
                throw new ArgumentException("Invalid JWToken.");
            }
            string encodedPayload = tokenParts[1];
            byte[] bytePayload = HttpServerUtility.UrlTokenDecode(encodedPayload);
            string jsonPayload = Encoding.UTF8.GetString(bytePayload);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonPayload);
        }

        private string _createToken(Dictionary<string, string> payload)
        {
            string token = "";
            string tokenType = "JWT";
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("alg", "SHA256");
            header.Add("typ", tokenType);
            string headerEncode = UrlBase64DictEncoding(header);
            string payloadEncode = UrlBase64DictEncoding(payload);
            string signEncode = GenerateTokenSignature(headerEncode, payloadEncode, key);
            Console.Out.WriteLine($"Header: {headerEncode} Payload: {payloadEncode}\n");
            token = $"{headerEncode}.{payloadEncode}.{signEncode}";
            return token;
        }

        public bool ValidateSignature(string token)
        {
            bool isValidated = false;
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

        private string UrlBase64DictEncoding(Dictionary<string, string> dict)
        {
            string jsonDict = JsonConvert.SerializeObject(dict);
            byte[] byteJsonDict = Encoding.UTF8.GetBytes(jsonDict);
            string base64EncodedDict = HttpServerUtility.UrlTokenEncode(byteJsonDict);
            return base64EncodedDict;
        }

        private string GenerateTokenSignature(string encodedHeader, 
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
