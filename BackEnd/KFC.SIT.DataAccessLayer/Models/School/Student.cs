using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class Student: User,IEntity
    {
        public Student()
        {
            Courses = new List<SchoolTeacherCourseStudent>();
        }

        public Student(int id)
        {
            Courses = new List<SchoolTeacherCourseStudent>();
        }

        public ICollection<SchoolTeacherCourseStudent> Courses { set; get; }
    }
}
