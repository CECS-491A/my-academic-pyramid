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
        List<SearchPersonDTO> SearchAllStudentsInSchool(int schoolId, string searchName);
        List<SearchPersonDTO> SearchAllTeachersInSchool(int schoolId, string searchName);
        List<ForumPostDTO> SearchAllForumPostsInSchool(int schoolId, string searchName);
        List<SearchPersonDTO> SearchStudentsInDepartment(int schoolId, int departmentId, string searchName);
        List<SearchPersonDTO> SearchTeachersInDepartment(int schoolId, int departmentId, string searchName);
        List<ForumPostDTO> SearchForumPostsInDepartment(int schoolId, int departmentId, string searchName);
        Student FindStudentByAccountId(int id);
        List<DepartmentDTO> GetDepartments(int schoolId);
        //IEnumerable<Question> SearchQuestions();
    }
}
