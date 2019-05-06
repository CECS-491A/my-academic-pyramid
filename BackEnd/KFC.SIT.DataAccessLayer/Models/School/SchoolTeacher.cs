using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.School
{
    public class SchoolTeacher : IEntity
    {
        public SchoolTeacher()
        {
            Courses = new List<SchoolTeacherCourse>();
        }

        public SchoolTeacher(int schoolId, int teacherId)
        {
            SchoolId = SchoolDepartment.SchoolId;
            TeacherId = teacherId;
            Courses = new List<SchoolTeacherCourse>();
        }

        [Key]
        public int Id { get; set; }

        //[ForeignKey("School")]
        public int SchoolId { get; set; }
        //public virtual School School { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        [ForeignKey("SchoolDepartment")]
        public int SchoolDepartmentId { get; set; }
        public virtual SchoolDepartment SchoolDepartment { get; set; }
        // [ForeignKey("Department"),Column(Order = 2)]
        // public int DepartmentId { get; set; }
        // [ForeignKey("Department"),Column(Order = 3)]
        // public int SchoolId2 { get; set; }
        // [ForeignKey("DepartmentId,SchoolId")]
        // public virtual SchoolDepartment Department { get; set; }

        public virtual ICollection<SchoolTeacherCourse> Courses { get; set; }
    }
}
