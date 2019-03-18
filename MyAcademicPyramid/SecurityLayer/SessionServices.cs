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
    }
}
