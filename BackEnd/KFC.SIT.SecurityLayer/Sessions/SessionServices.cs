using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace SecurityLayer.Sessions
{
    public class SessionServices
    {
        private DatabaseContext _db;
        public SessionServices(DatabaseContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }
            _db = db;
        }
        public void CreateSession(UserSession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }
            _db.Sessions.Add(session);

            _db.SaveChanges();
        }

        public void RefreshSession(string oldToken, string updatedToken, 
                                   DateTimeOffset currentDateTime, 
                                   DateTimeOffset expirationDateTime)
        {
            if (oldToken == null || currentDateTime == null 
                    || expirationDateTime == null)
            {
                throw new ArgumentNullException("A null argument was passed.");
            }
            UserSession sessionToUpdate = _db.Sessions.Where(s => s.Token == oldToken)
                                                      .FirstOrDefault();

            sessionToUpdate.Token = updatedToken;
            sessionToUpdate.RefreshedTime = currentDateTime;
            sessionToUpdate.ExpirationTime = expirationDateTime;
            _db.SaveChanges();
        }

        public string GetActiveSessionToken(int userid)
        {
            var foundSession = _db.Sessions.Where(s => s.UserId == userid)
                                           .Select(s => new {s.Token, s.RefreshedTime, s.ExpirationTime})
                                           .FirstOrDefault();
            string activeSessionToken = null;
            if (foundSession != null)
            {
                if (DateTimeOffset.UtcNow < foundSession.ExpirationTime)
                {
                    activeSessionToken = foundSession.Token;
                }
                else
                {
                    InvalidateSession(foundSession.Token);
                }
            }

            return activeSessionToken;
        }

        public bool IsInvalidated(string token)
        {
            // Get session id to determine if session exists. Get only session id
            // to keep the data received from database small.
            int? storedSessionId;
            try
            {
                storedSessionId = _db.Sessions.Where(s => s.Token == token)
                                              .Select(s => s.Id)
                                              .First();
            }
            catch (InvalidOperationException)
            {
                storedSessionId = null;
            }


            bool sessionNotStored = storedSessionId == null;

            return sessionNotStored;
        }

        public void InvalidateSession(string token)
        {
            UserSession sessionToDel = _db.Sessions.Where(s => s.Token == token)
                                                   .FirstOrDefault();
            if (sessionToDel != null)
            {
                _db.Sessions.Remove(sessionToDel);
                _db.SaveChanges();
            }
        }
    }
}
