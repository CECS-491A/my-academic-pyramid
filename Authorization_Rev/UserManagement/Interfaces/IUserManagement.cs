using System;
using System.Collections.Generic;
using System.Text;
using Authorization;
using Authorization.Interfaces;

namespace UserManagement.Interfaces
{
    public interface IUserManagement<CustomerUser> where CustomerUser:class, IUser
    {
    }
}
