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
    public class SchoolTeacherCourse
    {
        public SchoolTeacherCourse()
        {
            Students = new List<Student>();
        }

        public SchoolTeacherCourse(int schoolId, int teacherId, int departmentId, int courseId)
        {
            SchoolId = schoolId;
            TeacherId = teacherId;
            DepartmentId = departmentId;
            CourseId = courseId;
            Students = new List<Student>();
        }

        [Key, ForeignKey("SchoolTeacher"), Column(Order = 0)]
        public int SchoolId { get; set; }
        [Key, ForeignKey("SchoolTeacher"), Column(Order = 1)]
        public int TeacherId { get; set; }
        [ForeignKey("SchoolId, TeacherId")]
        public virtual SchoolTeacher SchoolTeacher { get; set; }

        [Key, ForeignKey("Course"), Column(Order = 3)]
        public int DepartmentId { get; set; }
        [Key, ForeignKey("Course"), Column(Order = 2)]
        public int CourseId { get; set; }
        [ForeignKey("Id, DepartmentId, SchoolId")]
        public virtual Course Course { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
