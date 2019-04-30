using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.School
{
    public class Course
    {
        public Course(string name, int schoolId, int departmentId, int teacherId)
        {
            Name = name;
            SchoolId = schoolId;
            DepartmentId = departmentId;
            TeacherId = teacherId;
            Students = new List<Student>();
        }

        [Required, Key]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [ForeignKey("Department")]
        public int DepartmentId { set; get; }
        public virtual Department Department { set; get; }
        [ForeignKey("School")]
        public int SchoolId { set; get; }
        public virtual School School { set; get; }
        [ForeignKey("Teacher")]
        public int TeacherId { set; get; }
        public virtual Teacher Teacher { set; get; }
        
        public virtual ICollection<Student> Students { set; get; }
    }
}
