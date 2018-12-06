using System.Data.Entity;

namespace DataAccessLayer.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context ) : base (context)
        {

        }
    }
}