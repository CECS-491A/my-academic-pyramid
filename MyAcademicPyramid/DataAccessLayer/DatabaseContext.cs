using System.Data.Entity;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        // Create a DbSet of user objects
        public virtual DbSet<User> Users { get; set; }
        
        //Create a Dbset of claim objects 
        public virtual DbSet<Claim> Claims { get; set; }


    }
}
