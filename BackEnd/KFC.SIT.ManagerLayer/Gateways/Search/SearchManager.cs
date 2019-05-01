using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models.Requests;
using DataAccessLayer.Models.School;
using ServiceLayer.Search;
using WebAPI.UserManagement;

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
        public dynamic Search(SearchRequest request, int category)
        {
            var student = ValidateRequest(request);

            if( request.SearchDepartment == 0)
            {
                switch (category)
                {
                    case 0:
                        var studentList = _searchService.SearchAllStudentsInSchool(student.SchoolId, request.SearchInput);
                        return studentList;
                    case 1:
                        var teacherList = _searchService.SearchAllTeachersInSchool(student.SchoolId, request.SearchInput);
                        return teacherList;
                    case 2:
                        var forumPostList = _searchService.SearchAllForumPostsInSchool(student.SchoolId, request.SearchInput);
                        return forumPostList;
                    default:
                        throw new ArgumentException("Invalid Search Category");
                }
                
            }
            else
            {
                Debug.WriteLine("HELLO" + category);
                switch (category)
                {
                    case 0:
                        var studentList = _searchService.SearchStudentsInDepartment(student.SchoolId, request.SearchDepartment, request.SearchInput);
                        return studentList;
                    case 1:
                        var teacherList = _searchService.SearchTeachersInDepartment(student.SchoolId, request.SearchDepartment, request.SearchInput);
                        return teacherList;
                    case 2:
                        var forumPostList = _searchService.SearchForumPostsInDepartment(student.SchoolId, request.SearchDepartment, request.SearchInput);
                        return forumPostList;
                    default:
                        throw new ArgumentException("Invalid Search Category");
                }
            }

            
            
        }

        

        public List<DepartmentDTO> GetDepartments(string AccountEmail)
        {
            if (AccountEmail is null)
            {
                throw new ArgumentNullException("User is not Logged In");
            }

            UserManager userManager = new UserManager();
            var account = userManager.FindByUserName(AccountEmail);

            if (account is null)
            {
                throw new ArgumentException("User Account Does Not Exist");
            }

            var student = _searchService.FindStudentByAccountId(account.Id);

            if (student is null)
            {
                throw new ArgumentException("User is not a student");
            }

            var departmentList = _searchService.GetDepartments(student.SchoolId);
            if (departmentList is null)
            {
                throw new ArgumentException("No Departments Found");
            }
            return departmentList;
        }

        private Student ValidateRequest(SearchRequest request)
        {
            if (request.AccountEmail is null)
            {
                throw new ArgumentNullException("User is not Logged In");
            }
            if (request.SearchInput is null)
            {
                throw new ArgumentNullException("Search Input is Null");
            }

            UserManager userManager = new UserManager();
            var account = userManager.FindByUserName(request.AccountEmail);

            if (account is null)
            {
                throw new ArgumentException("User Account Does Not Exist");
            }

            var student = _searchService.FindStudentByAccountId(account.Id);

            if (student is null)
            {
                throw new ArgumentException("User is not a student");
            }

            return student;
        }
    }
}