using DataAccessLayer.DTOs;
using DataAccessLayer.Models.DiscussionForum;
using DataAccessLayer.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Search
{
    public interface ISearchService
    {
        List<SearchPersonDTO> GetStudents(Expression<Func<Student, bool>> predicate);
        List<SearchPersonDTO> GetTeachers(Expression<Func<SchoolTeacher, bool>> predicate);
        List<SearchForumPostDTO> GetForumPosts(Expression<Func<Question, bool>> predicate);

        Student FindStudentByAccountId(int id);
        List<DepartmentDTO> GetDepartments(int schoolId);
    }
}
