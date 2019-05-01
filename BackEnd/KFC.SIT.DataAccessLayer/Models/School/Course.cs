using DataAccessLayer.Models.DiscussionForum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.School
{
    public class Course :IEntity
    {
        public Course()
        {
            Questions = new List<Question>();
            Teachers = new List<SchoolTeacherCourse>();
        }

        public Course(string name, int schoolId, int departmentId)
        {
            Name = name;
            SchoolId = schoolId;
            DepartmentId = departmentId;
            Questions = new List<Question>();
            Teachers = new List<SchoolTeacherCourse>();
        }

        [Required, Key, Column(Order = 0)]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [ForeignKey("SchoolDepartment"),Column(Order = 2)]
        public int SchoolId { set; get; }
        [Key,ForeignKey("SchoolDepartment"), Column(Order = 1)]
        public int DepartmentId { set; get; }
        [ForeignKey("SchoolId, DepartmentId")]
        public virtual SchoolDepartment SchoolDepartment { set; get; }

        public virtual ICollection<SchoolTeacherCourse> Teachers { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
