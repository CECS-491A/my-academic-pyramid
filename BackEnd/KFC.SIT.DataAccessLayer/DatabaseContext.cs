using DataAccessLayer.Models;
using DataAccessLayer.Models.Messenger;
using DataAccessLayer.Models.School;
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

        public DbSet<Category> Categories { get; set; }

        //Set of Conservations
        public DbSet<Conversation> Conversations { get; set; }
        
        public DbSet<Message> Messages {get;set;}

        public DbSet<FriendRelationship>FriendRelationships { get; set; }

        public DbSet<ChatConnectionMapping> ChatConnectionMappings { get; set; }

        // School Tables
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolDepartment> SchoolDepartments { get; set; }
        public DbSet<SchoolTeacher> SchoolTeachers { get; set; }
        public DbSet<SchoolTeacherCourse> SchoolTeacherCourses { get; set; }
        public DbSet<SchoolTeacherCourseStudent> SchoolTeacherCourseStudents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DatabaseContext () :base ("name=LocalTest")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<DatabaseContext>());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer<DatabaseContext>(null);
            modelBuilder.Entity<User>()
                .HasOptional(p => p.ParentUser)
                .WithMany(p => p.ChildUsers)
                .HasForeignKey(p => p.ParentUser_Id);

        }



    }
}
