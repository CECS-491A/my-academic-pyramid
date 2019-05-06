using DataAccessLayer.Models;
using DataAccessLayer.Models.Messenger;
using DataAccessLayer.Models.School;
using DataAccessLayer.Models.DiscussionForum;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        // Set of users
        public DbSet<Account> Users { get; set; }
        // Set of Claims
        public DbSet<Claim> Claims { get; set; }

        public DbSet<UserSession> Sessions { get; set; }

        public DbSet<Category> Categories { get; set; }

        // Set of Conservations
        public DbSet<Conversation> Conversations { get; set; }
        
        // Set of Messages in conversation
        public DbSet<Message> Messages {get;set;}

        // Set of friends in a friendlist
        public DbSet<FriendRelationship>FriendRelationships { get; set; }

        // Set of SignalR connection Id
        public DbSet<ChatConnectionMapping> ChatConnectionMappings { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        // School Tables
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SchoolDepartment> SchoolDepartments { get; set; }
        public DbSet<SchoolTeacher> SchoolTeachers { get; set; }
        public DbSet<SchoolTeacherCourse> SchoolTeacherCourses { get; set; }

        public DatabaseContext () :base ("name=LocalTest")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer<DatabaseContext>(null);
            modelBuilder.Entity<Account>()
                .HasOptional(p => p.ParentUser)
                .WithMany(p => p.ChildUsers)
                .HasForeignKey(p => p.ParentUser_Id);
            
        }



    }
}
