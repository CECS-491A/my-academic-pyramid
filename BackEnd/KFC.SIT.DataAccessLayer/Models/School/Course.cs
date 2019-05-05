using DataAccessLayer.Models.DiscussionForum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.School
{
    public class Course
    {
        public Course()
        {
            //Questions = new List<Question>();
            Questions = new List<CourseQuestion>();
            Teachers = new List<SchoolTeacherCourse>();
        }

        public Course(string name, int schoolDepartmentId)
        {
            Name = name;
            //Questions = new List<Question>();
            Questions = new List<CourseQuestion>();
            Teachers = new List<SchoolTeacherCourse>();
        }

        [Required, Key]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [ForeignKey("SchoolDepartment"),Column(Order = 2)]
        public int SchoolDeparmentId { set; get; }
        public virtual SchoolDepartment SchoolDepartment { set; get; }

        public virtual ICollection<SchoolTeacherCourse> Teachers { get; set; }
        //public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<CourseQuestion> Questions { get; set; }
    }
}
