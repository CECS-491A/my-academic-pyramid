using System.Data.Entity;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        // Set of users
        public DbSet<User> Users { get; set; }
        // Set of Claims
        public DbSet<Claim> Claims { get; set; }

        public DatabaseContext () :base ("name=MYAPConnectionString")
        {

        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<User>()
                .HasOptional(p => p.ParentUser)
                .WithMany(p => p.ChildUsers)
                .HasForeignKey(p => p.ParentUser_Id);
   

            modelBuilder.Entity<User>().
                HasMany(c => c.Claims);
                
                
               

            base.OnModelCreating(modelBuilder);

        }
    }
}
