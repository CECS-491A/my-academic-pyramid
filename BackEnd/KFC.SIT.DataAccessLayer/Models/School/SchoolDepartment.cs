using DataAccessLayer.Models.DiscussionForum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.School
{
    public class SchoolDepartment : IEntity
    {
        public SchoolDepartment()
        {
            //Teachers = new List<SchoolTeacher>();
            Courses = new List<Course>();
            Students = new List<Student>();
            //Questions = new List<Question>();
            Questions = new List<DepartmentQuestion>();
            SchoolTeachers = new List<SchoolTeacher>();
        }

        public SchoolDepartment(int schoolId, int departmentId)
        {
            SchoolId = schoolId;
            DepartmentID = departmentId;
            //Teachers = new List<SchoolTeacher>();
            Courses = new List<Course>();
            Students = new List<Student>();
            //Questions = new List<Question>();
            Questions = new List<DepartmentQuestion>();
            SchoolTeachers = new List<SchoolTeacher>();
        }

        [Key]
        public int Id { get; set; } 

        [ForeignKey("School")]
        public int SchoolId { get; set; }
        public virtual School School { get; set; }
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        //public virtual ICollection<SchoolTeacher> Teachers { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        //public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<DepartmentQuestion> Questions { get; set; }
        public virtual ICollection<SchoolTeacher> SchoolTeachers { get; set; }
    }
}
