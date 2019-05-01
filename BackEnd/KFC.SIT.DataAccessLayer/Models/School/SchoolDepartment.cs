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
    public class SchoolDepartment
    {
        public SchoolDepartment()
        {
            Teachers = new List<SchoolTeacher>();
            Courses = new List<Course>();
            Students = new List<Student>();
            Questions = new List<Question>();
        }

        public SchoolDepartment(int schoolId, int departmentId)
        {
            SchoolId = schoolId;
            DepartmentID = departmentId;
            Teachers = new List<SchoolTeacher>();
            Courses = new List<Course>();
            Students = new List<Student>();
            Questions = new List<Question>();
        }

        [Key,ForeignKey("School"), Column(Order = 1)]
        public int SchoolId { get; set; }
        public virtual School School { get; set; }
        [Key,ForeignKey("Department"), Column(Order = 0)]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<SchoolTeacher> Teachers { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
