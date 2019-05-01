using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using ServiceLayer.UserManagement.UserAccountServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerLayer.sso
{
    public class SsoManager
    {
        private UserManagementServices _userManagementServices;
        private DatabaseContext _db;
        public SsoManager()
        {
            _db = new DatabaseContext();
            _userManagementServices = new UserManagementServices(_db);
        }

        public UserDTO DeleteUserBySsoId(Guid ssoId)
        {
            UserDTO deletedUser = null;
            int? userId = _userManagementServices.FindIdBySsoId(ssoId);
            if (userId != null)
            {
                User userToDelete = _userManagementServices.FindById((int)userId);
                deletedUser = new UserDTO()
                {
                    Id = userToDelete.Id,
                    UserName = userToDelete.UserName,
                    FirstName = userToDelete.FirstName,
                    LastName = userToDelete.LastName,
                    Category = userToDelete.Category.Value,
                    DateOfBirth = userToDelete.DateOfBirth.ToLongDateString(),
                    CreatedAt = userToDelete.CreatedAt.ToLongDateString()
                };
                _userManagementServices.DeleteUser(userToDelete);
                _db.SaveChanges();
            }

            return deletedUser;
        }

        public UserDTO FindUserById(Guid ssoId)
        {

            UserDTO foundUserDto = null;
            int? userId = _userManagementServices.FindIdBySsoId(ssoId);
            if (userId != null)
            {
                User foundUser = _userManagementServices.FindById((int)userId);
                foundUserDto = new UserDTO()
                {
                    Id = foundUser.Id,
                    UserName = foundUser.UserName,
                    FirstName = foundUser.FirstName,
                    LastName = foundUser.LastName,
                    Category = foundUser.Category.Value,
                    DateOfBirth = foundUser.DateOfBirth.ToLongDateString(),
                    Email = foundUser.Email,
                    CreatedAt = foundUser.CreatedAt.ToLongDateString()
                };
            }

            return foundUserDto;
        }
    }
    
}


