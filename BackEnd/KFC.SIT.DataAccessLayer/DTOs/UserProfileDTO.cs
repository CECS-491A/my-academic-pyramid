using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class UserProfileDTO
    {
        public string FirstName;
        public string MiddleName;
        public string LastName;
        public Nullable<int> Ranking;
        public string SchoolName;
        public string DepartmentName;
        public List<string> Courses;
        public bool AllowTelemetry;
    }
}
