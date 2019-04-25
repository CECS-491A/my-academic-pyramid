using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using SecurityLayer.utility;
using System.Security.Cryptography;
using Newtonsoft.Json;
using WebAPI.UserManagement;
using DataAccessLayer.Models;

namespace SecurityLayer.Sessions
{
    public class SessionManager
    {
        private DatabaseContext _db;
        private JWTokenManager _jwtManager;
        private SessionServices _sessionServices;
        private UserManager _userManager;
        private const double ACTIVE_SESSION_DURATION = 2.0;
        private const string SHARED_SSO_SECRET 
            = "D078F2AFC7E59885F3B6D5196CE9DB716ED459467182A19E04B6261BBC8E36EE";

        public SessionManager()
        {
            _db = new DatabaseContext();
            _jwtManager = new JWTokenManager();
            _sessionServices = new SessionServices(_db);
            _userManager = new UserManager();
        }

        public string CreateSession(int userid)
        {
            // Check if user already has a session. If so, return it.
            string token = null;
            string activeSessionToken = _sessionServices.GetActiveSessionToken(userid);
            if (activeSessionToken != null)
            {
                token = RefreshSession(activeSessionToken);
                return token;
            }
            Dictionary<string, string> payload = GeneratePayload(userid);
            SessionTimeStamp sessionTimeStamp = SetTime(payload);
            token = _jwtManager.CreateToken(payload);
            UserSession newSession = new UserSession()
            {
                Token = token,
                IsValid = true,
                CreationTime = sessionTimeStamp.CurrentTime,
                RefreshedTime = sessionTimeStamp.CurrentTime,
                ExpirationTime = sessionTimeStamp.ExpireTime,
                UserId = userid
            };
            _sessionServices.CreateSession(newSession);
            return token;
        }

        public bool ValidateSSOPayload(SSOPayload payload)
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

        public bool ValidateSession(string token)
        {
            bool isValid = (_jwtManager.VerifyHeader(token) 
                && _jwtManager.ValidateSignature(token)
                && !_sessionServices.IsInvalidated(token));
            if (isValid)
            {
                // Check if current time is less than expiredTime
                var payload = _jwtManager.DecodePayload(token);
                long expireTime;
                if (payload.ContainsKey(JWTokenManager.EXPIRATION_TIME_KEY))
                {
                    try
                    {
                        expireTime = long.Parse(
                            payload[JWTokenManager.EXPIRATION_TIME_KEY]
                        );
                        long currentTimeSec = DateTimeOffset.UtcNow
                                                            .ToUnixTimeSeconds();
                        isValid = currentTimeSec < expireTime;
                        if(!isValid)
                        {
                            // Remove session
                            _sessionServices.InvalidateSession(token);
                        }
                    }
                    catch (FormatException)
                    {
                        // Dictionary entry wasn't a long value.
                        // log it
                        isValid = false;
                    }
                }
                else
                {
                    // Payload doesn't contain an expiration entry.
                    isValid = false;
                }

            }
            return isValid;
        }

        public string RefreshSession(string encodedToken, Dictionary<string, string> payload)
        {
            // call SSO token
            if (!(payload.ContainsKey(JWTokenManager.EXPIRATION_TIME_KEY) 
                    && payload.ContainsKey(JWTokenManager.INITIAL_TIME_KEY) 
                    && payload.ContainsKey(JWTokenManager.REFRESH_TIME_KEY)))
            {
                // payload should contain the payload for an already created token.
                // If it doesn't have entries for initial time, refresh time, and
                // expiration time, then it's not a valid payload.
                throw new ArgumentException("payload is not valid payload for token.");
            }
            SessionTimeStamp sessionTimeStamp = SetTime(payload);
            string newToken = _jwtManager.CreateToken(payload);
            _sessionServices.RefreshSession(
                encodedToken, newToken, sessionTimeStamp.CurrentTime, 
                sessionTimeStamp.ExpireTime
            );

            return newToken;
        }

        public string RefreshSession(string encodedToken)
        {
            if(!_jwtManager.ValidateSignature(encodedToken))
            {
                throw new ArgumentException("encodedToken", "Not a valid JSON Web Token.");
            }
            Dictionary<string, string> payload = _jwtManager.DecodePayload(encodedToken);
            return this.RefreshSession(encodedToken, payload);
        }

        public string RefreshSessionUpdatedPayload(string encodedToken, int userid)
        {
            // Should only be used while registering a user.
            Dictionary<string, string> updatedPayload = GeneratePayload(userid);

            SessionTimeStamp sessionTimeStamp = SetTime(updatedPayload);
            string newToken = _jwtManager.CreateToken(updatedPayload);
            _sessionServices.RefreshSession(
                encodedToken, newToken, sessionTimeStamp.CurrentTime,
                sessionTimeStamp.ExpireTime
            );

            return newToken;
        }

        public void InvalidateSession(string token)
        {
            // Delete session from database.
            _sessionServices.InvalidateSession(token);
        }

        private SessionTimeStamp SetTime(Dictionary<string, string> payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException("Time can't be set in payload. " +
                                                "Payload is null.");
            }

            // Store time the session starts, when it expires, and when it's 
            // refreshed. Place each in payload to be stored in token.
            SessionTimeStamp timeStamp = new SessionTimeStamp();
            timeStamp.CurrentTime = DateTimeOffset.UtcNow;
            timeStamp.ExpireTime = timeStamp.CurrentTime.AddMinutes(
                ACTIVE_SESSION_DURATION
            );
            long currentTimeSec = timeStamp.CurrentTime.ToUnixTimeSeconds();
            long expTimeSec = timeStamp.ExpireTime.ToUnixTimeSeconds();
            payload[JWTokenManager.INITIAL_TIME_KEY] = currentTimeSec.ToString();
            payload[JWTokenManager.REFRESH_TIME_KEY] 
                = payload[JWTokenManager.INITIAL_TIME_KEY];
            payload[JWTokenManager.EXPIRATION_TIME_KEY] = expTimeSec.ToString();
            return timeStamp;
        }

        private Dictionary<string, string> GeneratePayload(int userid)
        {
            Dictionary<string, string> payload = new Dictionary<string, string>();
            User user = _userManager.FindUserById(userid);
            if (user == null)
            {
                throw new ArgumentException(
                    "There is no user with the given userid."
                );
            }
            payload["userid"] = user.Id.ToString();
            payload["username"] = user.UserName;
            List<string> claims = _userManager.GetClaims(user.UserName);
            string claimsJson = JsonConvert.SerializeObject(claims);
            payload["claims"] = claimsJson;
            return payload;
        }

    }
}
