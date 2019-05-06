using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.DTOs;
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
            // Validate
            var student = ValidateRequest(request);
            
            switch (request.SearchCategory)
            {
                // Search Students
                case 0:
                    var studentPredicate = PredicateBuilder.True<Student>();

                    // Match students' names with the search input
                    studentPredicate = studentPredicate.And(s => (s.Account.FirstName + " " + s.Account.MiddleName + " " + s.Account.LastName).Contains(request.SearchInput)
                        || (s.Account.FirstName + " " + s.Account.LastName).Contains(request.SearchInput));

                    // User logged in is not a student
                    if (student is null)
                    {
                        return _searchService.GetStudents(studentPredicate);
                    }
                    // Filter by school
                    studentPredicate = studentPredicate.And(s => s.SchoolDepartment.SchoolId == student.SchoolDepartment.SchoolId);
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
                    break;

                // Search Teachers
                case 1:
                    var teacherPredicate = PredicateBuilder.True<SchoolTeacher>();

                    // Match students' names with the search input
                    teacherPredicate = teacherPredicate.And(st => (st.Teacher.FirstName + " " + st.Teacher.MiddleName + " " + st.Teacher.LastName).Contains(request.SearchInput)
                    || (st.Teacher.FirstName + " " + st.Teacher.LastName).Contains(request.SearchInput));

                    // User logged in is not a student
                    if (student is null)
                    {
                        return _searchService.GetTeachers(teacherPredicate);
                    }
                    // Filter by school
                    teacherPredicate = teacherPredicate.And(st => st.SchoolDepartment.SchoolId == student.SchoolDepartment.SchoolId);
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
                    break;

                // Search Discussion Forum Posts
                case 2:
                    //var forumPostPredicate = PredicateBuilder.True<PostedQuestion>();

                    //// Match students' names with the search input
                    //forumPostPredicate = forumPostPredicate.And(q => q.Text.Contains(request.SearchInput));

                    //// User logged in is not a student
                    //if (student is null)
                    //{
                    //    return _searchService.GetForumPosts(forumPostPredicate);
                    //}
                    //// Filter by school
                    //if (request.SearchDepartment == 0)
                    //{
                    //    forumPostPredicate = forumPostPredicate.And(q => q.SchoolId == student.SchoolDepartment.SchoolId);
                    //    return _searchService.GetForumPosts(forumPostPredicate);
                    //}
                    //// Filter by department
                    //if (request.SearchCourse == 0)
                    //{
                    //    forumPostPredicate = forumPostPredicate.And(q => q.DepartmentId == request.SearchDepartment);
                    //    return _searchService.GetForumPosts(forumPostPredicate);
                    //}
                    break;
            }
            
            throw new ArgumentException("Invalid Search Category");

        }
        

        public List<DepartmentDTO> GetDepartments(int accountId)
        {

            UserManager userManager = new UserManager();
            var account = userManager.FindUserById(accountId);

            if (account is null)
            {
                throw new ArgumentException("User Account Does Not Exist");
            }

            var student = _searchService.FindStudentByAccountId(account.Id);

            if (student is null)
            {
                throw new ArgumentException("User is not a student");
            }

            var departmentList = _searchService.GetDepartments(student.SchoolDepartment.DepartmentID);
            if (departmentList is null)
            {
                throw new ArgumentException("No Departments Found");
            }
            return departmentList;
        }

        private Student ValidateRequest(SearchRequest request)
        {
            if (request.SearchInput is null)
            {
                throw new ArgumentNullException("Search Input is Null");
            }

            UserManager userManager = new UserManager();
            var account = userManager.FindUserById(request.AccountId);

            if (account is null)
            {
                throw new ArgumentException("User Account Does Not Exist");
            }

            var student = _searchService.FindStudentByAccountId(account.Id);

            return student;
        }
    }
}
