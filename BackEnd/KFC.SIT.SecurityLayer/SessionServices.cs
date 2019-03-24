using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace SecurityLayer
{
    public class SessionServices
    {
        private DatabaseContext _db;
        public SessionServices(DatabaseContext db)
        {
            _db = db;
        }
        public void CreateSession(UserSession session)
        {
            _db.Sessions.Add(session);
            _db.SaveChanges();
            // TODO add a catch in case operation failed.
        }

        public void RefreshSession(string oldToken, string updatedToken, 
                                   DateTimeOffset currentDateTime, 
                                   DateTimeOffset expirationDateTime)
        {
            
            UserSession sessionToUpdate = _db.Sessions.Where(s => s.Token == oldToken)
                                                      .FirstOrDefault();
            //DateTimeOffset currentTime = DateTimeOffset.UtcNow;
            //sessionToUpdate.RefreshedTime = currentTime.ToUnixTimeSeconds();
            sessionToUpdate.Token = updatedToken;
            sessionToUpdate.RefreshedTime = currentDateTime;
            sessionToUpdate.ExpirationTime = expirationDateTime;
            _db.SaveChanges();
        }

        public bool IsInvalidated(string token)
        {
            /* Get the isValid attribute of the token
             * If true, return true. Else, return false.
             */
            bool? isValid = _db.Sessions.Where(s => s.Token == token)
                                       .Select(s => s.IsValid)
                                       .FirstOrDefault();
            return (isValid == null || isValid == false);
              
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
