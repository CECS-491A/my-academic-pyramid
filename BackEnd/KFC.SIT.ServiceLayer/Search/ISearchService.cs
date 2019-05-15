using DataAccessLayer.DTOs;
using DataAccessLayer.DTOs.SearchDTO;
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

        List<SearchForumPostDTO> GetSchoolQuestions(Expression<Func<SchoolQuestion, bool>> predicate);
        List<SearchForumPostDTO> GetDepartmentQuestions(Expression<Func<DepartmentQuestion, bool>> predicate);
        List<SearchForumPostDTO> GetCourseQuestions(Expression<Func<CourseQuestion, bool>> predicate);
        
        List<SearchFilterSelectionDTO> GetSchools();
        List<SearchFilterSelectionDTO> GetDepartments(int schoolId);
        List<SearchFilterSelectionDTO> GetCourses(int schoolId, int departmentId);

        Course GetCourse(int courseId);
    }
}
