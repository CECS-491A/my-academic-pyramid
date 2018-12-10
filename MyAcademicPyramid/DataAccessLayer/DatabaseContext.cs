using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }

        public DatabaseContext () :base ()
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
