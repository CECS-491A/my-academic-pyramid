using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class SchoolTeacherCourseStudent : IEntity
    {
        public SchoolTeacherCourseStudent()
        {

        }

        [Required, Key]
        public int Id { set; get; }
        [Required]
        public int SchoolTeacherCourseId { set; get; }
        [Required]
        public int StudentId { set; get; }
    }
}
