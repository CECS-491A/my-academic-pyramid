using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class SchoolTeacher
    {
        public SchoolTeacher(int id, int schoolId, int teacherId, int schoolDepartmentId)
        {
            Id = id;
            SchoolId = schoolId;
            TeacherId = teacherId;
            SchoolDepartmentId = schoolDepartmentId;
            Courses = new List<SchoolTeacherCourse>();
        }

        [Required, Key]
        public int Id { set; get; }
        [Required]
        public int SchoolId { set; get; }
        [Required]
        public int TeacherId { set; get; }
        
        public int SchoolDepartmentId { set; get; }

        public ICollection<SchoolTeacherCourse> Courses { set; get; }
    }
}
