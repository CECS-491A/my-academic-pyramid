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
            bool isValid = (VerifyHeader(token) && ValidateSignature(token) 
                            && !_sessionServices.IsInvalidated(token));
            // TODO check the header as well
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

        public bool VerifyHeader(string jwtoken)
        {
            string tokenTypeKeyStr = "typ";
            string algKeyStr = "alg";
            string validTokenType = "JWT";
            string validAlgorithm = "SHA256";
            string headerStr = jwtoken.Split('.')[0];
            Dictionary<string, string> headerDict 
                = DecodeBase64EncodedDict(headerStr);

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
            return DecodeBase64EncodedDict(encodedPayload);
        }

        private Dictionary<string, string> DecodeBase64EncodedDict(
            string urlSafeBase64EncodedStr
        )
        {
            // TODO make this return null if exception is raised.
            string base64Str = FromUrlSafeBase64Str(urlSafeBase64EncodedStr);
            byte[] byteDict = null;
            try
            {
                byteDict = System.Convert.FromBase64String(base64Str);
            }
            catch (FormatException)
            {
                // base64Str is not a valid base64 encoded string.
                return null;
            }
            
            string jsonDict = Encoding.UTF8.GetString(byteDict);
            Dictionary<string, string> resultDict 
                = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonDict);
            return resultDict;
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
            //string base64EncodedDict = HttpServerUtility.UrlTokenEncode(byteJsonDict);
            string base64EncodedDict = ToUrlSafeBase64Str(byteJsonDict);
            
            return base64EncodedDict;
        }

        private string ToUrlSafeBase64Str(byte []byteArray)
        {
            string result = "";
            string temp = System.Convert.ToBase64String(byteArray);
            // Remove ='s at the end that were caused by padding the last
            // group of bytes to be 24 bits long.
            temp = temp.TrimEnd('=');
            // Replace + with - and / with _ to make string URL safe
            result = temp.Replace("+", "-").Replace("/", "_");
            return result;
        }

        private string FromUrlSafeBase64Str(string urlSafeBase64EncodedStr)
        {
            string result = "";
            string temp;
            string charPad = "=";
            int remainder = urlSafeBase64EncodedStr.Length % 4;
            switch(remainder)
            {
                // Remainder of 3 should never occur because a byte group
                // has a minimum of 8 bits which should produce two base64 chars.
                case 2:
                    temp = urlSafeBase64EncodedStr + charPad + charPad;
                    break;
                case 1:
                    temp = urlSafeBase64EncodedStr + charPad;
                    break;
                default:
                    temp = urlSafeBase64EncodedStr;
                    break;
            }
            result = temp.Replace("_", "/").Replace("-", "+");
            return result;
        }

        private string GenerateTokenSignature(string encodedHeader, 
                                                     string encodedPayload,
                                                     string key)
        {
            byte[] preSign = Encoding.UTF8.GetBytes(encodedHeader + "." + encodedPayload);
            byte[] byteKey = Encoding.UTF8.GetBytes(key);
            HMACSHA256 hmac = new HMACSHA256(byteKey);
            byte[] signature = hmac.ComputeHash(preSign);
            //return HttpServerUtility.UrlTokenEncode(signature);
            return ToUrlSafeBase64Str(signature);
        }

        public void InvalidateToken(string token)
        {
            // Invalidate token from database
            // get the instance in the database
            // set valid to false.
            // save
            _sessionServices.InvalidateSession(token);
        }

        public static string GenerateToken(String jsonPayload, string algorithm)
        {
            string token = "";

            return token;
        }
    }
}
