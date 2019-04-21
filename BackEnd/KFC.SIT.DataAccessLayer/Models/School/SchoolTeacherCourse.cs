using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class SchoolTeacherCourse
    {
        public SchoolTeacherCourse(int id, int schoolTeacherId, int courseId)
        {
            Id = id;
            SchoolTeacherId = schoolTeacherId;
            CourseId = courseId;
            Students = new List<SchoolTeacherCourseStudent>();
            //Questions = new List<Question>();
        }

        [Required, Key]
        public int Id { set; get; }
        [Required]
        public int SchoolTeacherId { set; get; }
        [Required]
        public int CourseId { set; get; }
        
        public ICollection<SchoolTeacherCourseStudent> Students { set; get; }
        //public ICollection<Question> Questions { set; get; }
    }
}
