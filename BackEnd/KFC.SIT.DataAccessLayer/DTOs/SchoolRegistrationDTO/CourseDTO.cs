using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.SchoolRegistrationDTO
{
    public class CourseDTO
    {
        public string CourseName { set; get; }
        public string SchoolName { get; set; }
        public string DepartmentName { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
    }
}
