using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public interface IUserManagement
    {
         void EnableUser();
         void DisableUser();
         void DeleteOtherAccount();
         void DeleteUserPost();
         void DeleteUserOwnAccount();
    }
}
