using DataAccessLayer.DTOs;
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
        List<SearchDTO> SearchStudents(string searchInput);
        List<SearchDTO> SearchTeachers(string searchInput);
    }
}
