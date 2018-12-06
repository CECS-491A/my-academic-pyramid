using System.Data.Entity;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Claim> Claims { get; set; }


    }
}
