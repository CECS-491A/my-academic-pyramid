using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.School
{
    public class SchoolTeacher
    {
        public SchoolTeacher()
        {
            Courses = new List<SchoolTeacherCourse>();
        }

        public SchoolTeacher(int schoolId, int teacherId)
        {
            SchoolId = schoolId;
            TeacherId = teacherId;
            Courses = new List<SchoolTeacherCourse>();
        }

        [Key,ForeignKey("School"), Column(Order = 0)]
        public int SchoolId { get; set; }
        public virtual School School { get; set; }
        [Key,ForeignKey("Teacher"), Column(Order = 1)]
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("Department"),Column(Order = 2)]
        public int DepartmentId { get; set; }
        [ForeignKey("Department"),Column(Order = 3)]
        public int SchoolId2 { get; set; }
        [ForeignKey("DepartmentId,SchoolId")]
        public virtual SchoolDepartment Department { get; set; }

        public virtual ICollection<SchoolTeacherCourse> Courses { get; set; }
    }
}
