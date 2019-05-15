using DataAccessLayer.DTOs;
using DataAccessLayer.DTOs.SearchDTO;
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
        dynamic Search (SearchRequest request);
        List<SearchFilterSelectionDTO> GetSchools();
        List<SearchFilterSelectionDTO> GetDepartments(int schoolId);
        List<SearchFilterSelectionDTO> GetCourses(int schoolId, int departmentId);
    }
}
