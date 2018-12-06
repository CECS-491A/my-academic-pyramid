using DataAccessLayer;
using System.Collections.Generic;

namespace DataAccessLayer.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository() : base (new List<User> ())
        {

        }
    }
}