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
        List<SearchDTO> SearchStudents(string searchName);
        List<SearchDTO> SearchTeachers(string searchName);
        //IEnumerable<Question> SearchQuestions();
    }
}
