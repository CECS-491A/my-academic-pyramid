using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.School
{
    public class Student
    {
        public Student()
        {
            Courses = new List<SchoolTeacherCourse>();
        }

        public Student(int accountId, int schoolDepartmentId)
        {
            AccountId = accountId;
            SchoolDepartmentId = schoolDepartmentId;
            Courses = new List<SchoolTeacherCourse>();
        }
        
        [Key]
        public int Id { set; get; }

        [ForeignKey("Account")]
        public int AccountId { set; get; }
        public virtual Account Account { set; get; }

        [ForeignKey("SchoolDepartment")]
        public int SchoolDepartmentId { get; set; }
        public virtual SchoolDepartment SchoolDepartment { get; set; }
        
        public virtual ICollection<SchoolTeacherCourse> Courses { get; set; }
    }
}
