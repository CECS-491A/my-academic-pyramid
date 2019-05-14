using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.DTOs.SearchDTO;
using DataAccessLayer.Models.DiscussionForum;
using DataAccessLayer.Models.Requests;
using DataAccessLayer.Models.School;
using ServiceLayer;
using ServiceLayer.Search;
using WebAPI.Gateways.UserManagement;

namespace ManagerLayer.Gateways.Search
{
    public class SearchManager : ISearchManager
    {

        private DatabaseContext _db;
        private ISearchService _searchService;

        public SearchManager(DatabaseContext db)
        {
            _db = db;
            _searchService = new SearchService(_db);
        }

        // TODO: Make all messages constants
        public dynamic Search(SearchRequest request)
        {
            if (request.SearchInput is null)
            {
                throw new ArgumentNullException("Search Input is Null");
            }

            switch (request.SearchCategory)
            {
                // Search Students
                case 0:
                    var studentPredicate = PredicateBuilder.True<Student>();

                    // Match students' names with the search input
                    studentPredicate = studentPredicate.And(s => (s.Account.FirstName + " " + s.Account.MiddleName + " " + s.Account.LastName).Contains(request.SearchInput)
                        || (s.Account.FirstName + " " + s.Account.LastName).Contains(request.SearchInput));

                    // User logged in is not a student
                    if (request.SearchSchool == 0)
                    {
                        return _searchService.GetStudents(studentPredicate);
                    }
                    // Filter by school
                    studentPredicate = studentPredicate.And(s => s.SchoolDepartment.SchoolId == request.SearchSchool);
                    if (request.SearchDepartment == 0)
                    {
                        return _searchService.GetStudents(studentPredicate);
                    }
                    // Filter by department
                    studentPredicate = studentPredicate.And(s => s.SchoolDepartment.DepartmentID == request.SearchDepartment);
                    if (request.SearchCourse == 0)
                    {
                        return _searchService.GetStudents(studentPredicate);
                    }

                    var students = _searchService.GetStudents(studentPredicate);
                    var studentCourse = _searchService.GetCourse(request.SearchCourse);

                    var filteredStudents = new List<SearchPersonDTO>();

                    for(int i = 0; i < students.Count; i++)
                    {
                        if(students[i].Courses != null)
                        {
                            for (int j = 0; j < students[i].Courses.Count; j++)
                            {
                                if (students[i].Courses[j].Equals(studentCourse.Name))
                                {
                                    filteredStudents.Add(students[i]);
                                    break;
                                }
                            }
                        }
                        
                    }

                    return filteredStudents;

                // Search Teachers
                case 1:
                    var teacherPredicate = PredicateBuilder.True<SchoolTeacher>();

                    // Match students' names with the search input
                    teacherPredicate = teacherPredicate.And(st => (st.Teacher.FirstName + " " + st.Teacher.MiddleName + " " + st.Teacher.LastName).Contains(request.SearchInput)
                    || (st.Teacher.FirstName + " " + st.Teacher.LastName).Contains(request.SearchInput));

                    // User logged in is not a student
                    if (request.SearchSchool == 0)
                    {
                        return _searchService.GetTeachers(teacherPredicate);
                    }
                    // Filter by school
                    teacherPredicate = teacherPredicate.And(st => st.SchoolDepartment.SchoolId == request.SearchSchool);
                    if (request.SearchDepartment == 0)
                    {
                        return _searchService.GetTeachers(teacherPredicate);
                    }
                    // Filter by department
                    teacherPredicate = teacherPredicate.And(st => st.SchoolDepartment.DepartmentID == request.SearchDepartment);
                    if (request.SearchCourse == 0)
                    {
                        return _searchService.GetTeachers(teacherPredicate);
                    }

                    var teachers = _searchService.GetTeachers(teacherPredicate);
                    var teacherCourse = _searchService.GetCourse(request.SearchCourse);

                    var filteredTeachers = new List<SearchPersonDTO>();

                    for (int i = 0; i < teachers.Count; i++)
                    {
                        if(teachers[i].Courses != null)
                        {
                            for (int j = 0; j < teachers[i].Courses.Count; j++)
                            {
                                if (teachers[i].Courses[j].Equals(teacherCourse.Name))
                                {
                                    filteredTeachers.Add(teachers[i]);
                                    break;
                                }
                            }
                        }
                    }

                    return filteredTeachers;

                // Search Discussion Forum Posts
                case 2:

                    if(request.SearchDepartment == 0)
                    {
                        var schoolQuestionPredicate = PredicateBuilder.True<SchoolQuestion>();
                        schoolQuestionPredicate = schoolQuestionPredicate.And(q => q.Text.Contains(request.SearchInput));
                        schoolQuestionPredicate = schoolQuestionPredicate.And(q => q.SchoolId == request.SearchSchool);
                        return _searchService.GetSchoolQuestions(schoolQuestionPredicate);
                    }

                    if(request.SearchCourse == 0)
                    {
                        var departmentQuestionPredicate = PredicateBuilder.True<DepartmentQuestion>();
                        departmentQuestionPredicate = departmentQuestionPredicate.And(q => q.Text.Contains(request.SearchInput));
                        departmentQuestionPredicate = departmentQuestionPredicate.And(q => q.SchoolDepartment.SchoolId == request.SearchSchool);
                        departmentQuestionPredicate = departmentQuestionPredicate.And(q => q.SchoolDepartment.DepartmentID == request.SearchDepartment);
                        return _searchService.GetDepartmentQuestions(departmentQuestionPredicate);
                    }

                    var courseQuestionPredicate = PredicateBuilder.True<CourseQuestion>();
                    courseQuestionPredicate = courseQuestionPredicate.And(q => q.Text.Contains(request.SearchInput));
                    courseQuestionPredicate = courseQuestionPredicate.And(q => q.Course.SchoolDepartment.SchoolId == request.SearchSchool);
                    courseQuestionPredicate = courseQuestionPredicate.And(q => q.Course.SchoolDepartment.DepartmentID == request.SearchDepartment);
                    courseQuestionPredicate = courseQuestionPredicate.And(q => q.CourseId == request.SearchCourse);
                    return _searchService.GetCourseQuestions(courseQuestionPredicate);
            }
            
            throw new ArgumentException("Invalid Search Category");

        }
        
        public List<SearchFilterSelectionDTO> GetSchools()
        {
            return _searchService.GetSchools();
        }

        public List<SearchFilterSelectionDTO> GetDepartments(int schoolId)
        {
            return _searchService.GetDepartments(schoolId);
        }

        public List<SearchFilterSelectionDTO> GetCourses(int schoolId, int departmentId)
        {
            return _searchService.GetCourses(schoolId, departmentId);
        }

        public AccountDTO GetAccount(int accountId)
        {
            UserManager userManager = new UserManager();
            var account = userManager.FindUserById(accountId);

            if (account is null)
            {
                throw new ArgumentException("User Account Does Not Exist");
            }

            var student = _searchService.FindStudentByAccountId(account.Id);

            if(student is null)
            {
                return new AccountDTO();
            }

            return new AccountDTO
            {
                IsStudent = true,
                SchoolId = student.SchoolDepartment.SchoolId
            };
        }
    }
}
