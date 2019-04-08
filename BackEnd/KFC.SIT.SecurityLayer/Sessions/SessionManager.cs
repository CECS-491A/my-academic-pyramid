﻿using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.UserManagement.UserAccountServices;
using SecurityLayer.utility;

namespace SecurityLayer.Sessions
{
    public class SessionManager
    {
        private DatabaseContext _db;
        private JWTokenManager _jwtManager;
        private SessionServices _sessionServices;
        private UserManagementServices _umServices;
        private const double ACTIVE_SESSION_DURATION = 2.0;

        public SessionManager()
        {
            _db = new DatabaseContext();
            _jwtManager = new JWTokenManager();
            _sessionServices = new SessionServices(_db);
            _umServices = new UserManagementServices(_db);
        }

        public string CreateSession(int userid)
        {
            // Check if user already has a session. If so, return it.
            string token = null;
            UserSession session = _sessionServices.GetActiveSession(userid);
            if (session != null)
            {
                token = session.Token;
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
            User user = _umServices.FindById(userid);
            if (user == null)
            {
                throw new ArgumentException(
                    "There is no user with the given userid."
                );
            }
            payload["username"] = user.UserName;
            return payload;
        }

    }
}
