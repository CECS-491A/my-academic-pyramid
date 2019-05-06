using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.SchoolRegistrationDTO
{
    public class TeacherDTO
    {
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        public string LastName { set; get; }
        public string DepartmentName { get; set; }
     
    }
}
