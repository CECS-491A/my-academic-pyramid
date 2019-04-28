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
        IQueryable SearchStudents(string searchName);
        IQueryable SearchTeachers(string searchName);
        //IEnumerable<Question> SearchQuestions();
    }
}
