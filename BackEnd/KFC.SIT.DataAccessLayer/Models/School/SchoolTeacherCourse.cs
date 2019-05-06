using DataAccessLayer.Models.DiscussionForum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.School
{
    public class SchoolTeacherCourse : IEntity
    {
        public SchoolTeacherCourse()
        {
            Students = new List<Student>();
        }

        public SchoolTeacherCourse(int schoolteacherId, int courseId)
        {
            SchoolTeacherId = schoolteacherId;
            CourseId = courseId;
            Students = new List<Student>();
        }

        [Key, Required]
        public int Id { get; set; }

       
        public virtual SchoolTeacher SchoolTeacher { get; set; }
        [ForeignKey("SchoolTeacher")]
        public int SchoolTeacherId { get; set; }

        
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
