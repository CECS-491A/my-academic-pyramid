using DataAccessLayer.DTOs;
using DataAccessLayer.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Search
{
    public interface ISearchService
    {
        List<SearchPersonDTO> SearchStudents(int schoolId, string searchName);
        List<SearchPersonDTO> SearchTeachers(int schoolId, string searchName);
        List<Department> GetDepartments(int schoolId);
        //IEnumerable<Question> SearchQuestions();
    }
}
