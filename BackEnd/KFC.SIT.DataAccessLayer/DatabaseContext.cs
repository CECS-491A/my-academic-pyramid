using DataAccessLayer.Models;
using DataAccessLayer.Models.Messenger;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        // Set of users
        public DbSet<User> Users { get; set; }
        // Set of Claims
        public DbSet<Claim> Claims { get; set; }

        public DbSet<UserSession> Sessions { get; set; }

        public DbSet<Catergory> Catergories { get; set; }

        //Set of Conservations
        public DbSet<Conversation> Conservations { get; set; }

        public DbSet<MessengerContactHist> MessengerContactHists {get;set;}

        public DbSet<FriendRelationship>FriendRelationships { get; set; }

        public DbSet<ChatConnectionMapping> ChatConnectionMappings { get; set; }

        public DatabaseContext () :base ("name=LocalTest")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<User>()
                .HasOptional(p => p.ParentUser)
                .WithMany(p => p.ChildUsers)
                .HasForeignKey(p => p.ParentUser_Id);

     
                





        }


    }
}
