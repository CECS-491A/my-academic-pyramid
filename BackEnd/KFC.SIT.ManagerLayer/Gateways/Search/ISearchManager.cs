using DataAccessLayer.DTOs;
using DataAccessLayer.Models.Requests;
using DataAccessLayer.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Gateways.Search
{
    public interface ISearchManager
    {
        List<SearchPersonDTO> SearchStudents(SearchRequest request);
        List<SearchPersonDTO> SearchTeachers(SearchRequest request);
        List<Department> GetDepartments(SearchRequest request);
    }
}
