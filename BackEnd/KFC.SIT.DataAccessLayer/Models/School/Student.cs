using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.School
{
    public class Student : User, IEntity
    {
        public Student()
        {

        }
        public Student(int schoolId, int departmentId)
        {
            SchoolId = schoolId;
            DepartmentId = departmentId;

            Courses = new List<Course>();
        }

        [ForeignKey("School")]
        public int SchoolId { set; get; }
        public virtual School School { set; get; }
        [ForeignKey("Department")]
        public int DepartmentId { set; get; }
        public virtual Department Department { set; get; }

        public virtual ICollection<Course> Courses { set; get; }
    }
}
