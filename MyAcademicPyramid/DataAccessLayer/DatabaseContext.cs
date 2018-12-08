
using System.Data.Entity;


namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
       public DatabaseContext():base()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOptional(s => s.ParentUser);
                
         
            
            modelBuilder.Entity<User>().
              HasOptional(e => e.ChildrenUsers).
              WithMany().
              HasForeignKey(m => m.ChildrenUserId);
        }

        // Create a DbSet of user objects
        public virtual DbSet<User> Users { get; set; }
        
        //Create a Dbset of claim objects 
    public virtual DbSet<Claim> Claims { get; set; }

    }
}
