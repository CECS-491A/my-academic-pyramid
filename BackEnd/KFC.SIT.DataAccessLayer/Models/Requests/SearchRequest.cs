using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Requests
{
    public class SearchRequest
    {
        public int AccountId { get; set; }
        public int SearchCategory { get; set; }
        public int SearchSchool { get; set; }
        public int SearchDepartment { get; set; }
        public int SearchCourse { get; set; }
        public string SearchInput { get; set; }
    }
}
