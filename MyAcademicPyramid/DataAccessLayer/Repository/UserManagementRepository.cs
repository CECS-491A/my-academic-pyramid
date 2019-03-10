using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class UserManagementRepository
    {
        public User CreateNewUser(DatabaseContext _db, User user)
        {
            _db.Entry(user).State = EntityState.Added;
            return user;
        }

        public User DeleteUser(DatabaseContext _db, Guid Id)
        {
            var user = _db.Users
                .Where(c => c.Id == Id)
                .FirstOrDefault<User>();
            if (user == null)
                return null;
            _db.Entry(user).State = EntityState.Deleted;
            return user;
        }

        public User GetUser(DatabaseContext _db, string email)
        {
            var user = _db.Users
                .Where(c => c.Email == email)
                .FirstOrDefault<User>();
            return user;
        }

        public User GetUser(DatabaseContext _db, Guid Id)
        {
            return _db.Users.Find(Id);
        }

        public User UpdateUser(DatabaseContext _db, User user)
        {
            user.UpdatedAt = DateTime.UtcNow;
            _db.Entry(user).State = EntityState.Modified;
            return user;
        }

        public bool ExistingUser(DatabaseContext _db, User user)
        {
            var result = GetUser(_db, user.Email);
            if (result != null)
            {
                return true;
            }
            return false;
        }

        public bool ExistingUser(DatabaseContext _db, string email)
        {
            var result = GetUser(_db, email);
            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
}
