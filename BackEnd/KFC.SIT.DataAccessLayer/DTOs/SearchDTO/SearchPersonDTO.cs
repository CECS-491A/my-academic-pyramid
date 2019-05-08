using DataAccessLayer.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.SearchDTO
{
    public class SearchPersonDTO
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string SchoolName { get; set; }
        public string DepartmentName { get; set; }
        public List<string> Courses { get; set; }
    }
}
