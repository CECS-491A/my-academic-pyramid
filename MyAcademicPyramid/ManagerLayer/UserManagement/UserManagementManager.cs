using System;
using DataAccessLayer.Models;
using DataAccessLayer;
using ServiceLayer;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.MockUserManagementNameSpace
{
    public class UserManagementManager : IUserManagement
    {
        private IPasswordService _passwordService;
        private IUserService _userService;

        public UserManagementManager()
        {
            _userService = new UserManagementServices();
        }

        private DatabaseContext CreateDbContext()
        {
            return new DatabaseContext();
        }

        public User CreateUser(string email, string password, DateTime dob)
        {
            try
            {
                var valid = new System.Net.Mail.MailAddress(email);
            }
            catch (Exception)
            {
                return null;
            }
            _passwordService = new PasswordService();
            DateTime timestamp = DateTime.UtcNow;
            byte[] salt = _passwordService.GenerateSalt();
            string hash = _passwordService.HashPassword(password, salt);
            User user = new User
            {
                Email = email,
                PasswordHash = hash,
                PasswordSalt = salt,
                DateOfBirth = dob,
                UpdatedAt = timestamp
            };

            using (var _db = CreateDbContext())
            {
                var response = _userService.CreateUser(_db, user);
                try
                {
                    _db.SaveChanges();
                    return user;
                }
                catch (DbEntityValidationException ex)
                {
                    //catch error
                    // detach user attempted to be created from the db context - rollback
                    _db.Entry(response).State = System.Data.Entity.EntityState.Detached;
                }
            }
            return null;
        }

        public int DeleteUser(User user)
        {
            using (var _db = CreateDbContext())
            {
                var response = _userService.DeleteUser(_db, user.Id);
                // will return null if user does not exist
                return _db.SaveChanges();
            }
        }

        public int DeleteUser(Guid id)
        {
            using (var _db = CreateDbContext())
            {
                var response = _userService.DeleteUser(_db, id);
                return _db.SaveChanges();
            }
        }

        public User GetUser(Guid id)
        {
            using (var _db = CreateDbContext())
            {
                return _userService.GetUser(_db, id);
            }
        }

        public User GetUser(string email)
        {
            using (var _db = CreateDbContext())
            {
                return _userService.GetUser(_db, email);
            }
        }

        public int DisableUser(User user)
        {
            return ToggleUser(user, false);
        }

        public int EnableUser(User user)
        {
            return ToggleUser(user, true);
        }

        public int ToggleUser(User user, bool? enable)
        {
            using (var _db = CreateDbContext())
            {
                if (enable == null) enable = !user.Disabled;
                user.Disabled = !(bool)enable;
                var response = _userService.UpdateUser(_db, user);
                return _db.SaveChanges();
            }
        }

        public int UpdateUser(User user)
        {
            using (var _db = CreateDbContext())
            {
                var response = _userService.UpdateUser(_db, user);
                try
                {
                    return _db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    // catch error
                    // rollback changes
                    _db.Entry(response).CurrentValues.SetValues(_db.Entry(response).OriginalValues);
                    _db.Entry(response).State = System.Data.Entity.EntityState.Unchanged;
                    return 0;
                }
            }
        }
    }
}