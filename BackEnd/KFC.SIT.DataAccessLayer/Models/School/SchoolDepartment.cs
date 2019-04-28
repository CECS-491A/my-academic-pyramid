using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class SchoolDepartment
    {
        public SchoolDepartment(int id, int schoolId, int departmentId)
        {
            Id = id;
            SchoolId = schoolId;
            DepartmentId = departmentId;

            Teachers = new List<SchoolTeacher>();
            Courses = new List<Course>();
        }

        [Required, Key]
        public int Id { set; get; }
        [Required]
        public int SchoolId { set; get; }
        [Required]
        public int DepartmentId { set; get; }

        ICollection<SchoolTeacher> Teachers { set; get; }
        ICollection<Course> Courses { set; get; }
        ICollection<Student> Students { set; get; }
    }
}
