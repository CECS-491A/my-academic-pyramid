using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
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
        public UserSession GetSession(string token)
        {
            UserSession session = _db.Sessions.FirstOrDefault(s => s.Token == token);
            return session;
        }
    }
}
