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

        public Student(int accountId, int schoolId, int departmentId)
        {
            AccountId = accountId;
            SchoolId = schoolId;
            DepartmentId = departmentId;
            Courses = new List<SchoolTeacherCourse>();
        }
        
        [Key, Column(Order =0)]
        public int Id { set; get; }
        [ForeignKey("Account")]
        public int AccountId { set; get; }
        public virtual Account Account { set; get; }

        [ForeignKey("SchoolDepartment"),Column(Order =2)]
        public int SchoolId { get; set; }
        [ForeignKey("SchoolDepartment"),Column(Order = 1)]
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId, SchoolId")]
        public virtual SchoolDepartment SchoolDepartment { get; set; }
        
        public virtual ICollection<SchoolTeacherCourse> Courses { get; set; }
    }
}
