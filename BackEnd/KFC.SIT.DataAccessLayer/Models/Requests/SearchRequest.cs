using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Requests
{
    public class SearchRequest
    {
        public string AccountEmail { get; set; }
        public int SearchCategory { get; set; }
        public int SearchDepartment { get; set; }
        public string SearchInput { get; set; }
    }
}
