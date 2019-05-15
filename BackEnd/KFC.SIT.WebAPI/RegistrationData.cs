using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFC.SIT.WebAPI
{
    public class RegistrationData
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int SchoolId { get; set; }
        public int DepartmentId { get; set; }
        public List<int> selectedCourseIds { get; set; }
    }
}