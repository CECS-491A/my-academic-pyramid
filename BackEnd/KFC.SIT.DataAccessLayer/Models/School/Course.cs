using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class Course
    {
        public Course(int id, string name, int schoolDepartmentId)
        {
            Id = id;
            Name = name;
            SchoolDepartmentId = schoolDepartmentId;

            Teachers = new List<SchoolTeacherCourse>();
        }

        [Required, Key]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public int SchoolDepartmentId { set; get; }
        
        public ICollection<SchoolTeacherCourse> Teachers { set; get; }
    }
}
