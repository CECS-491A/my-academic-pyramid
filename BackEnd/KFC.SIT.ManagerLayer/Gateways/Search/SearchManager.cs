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
        public List<SearchPersonDTO> SearchStudents(SearchRequest request)
        {
            if(request.AccountEmail is null)
            {
                throw new ArgumentNullException("User is not Logged In");
            }
            if (request.SearchInput is null)
            {
                throw new ArgumentNullException("Search Input is Null");
            }

            UserManager userManager = new UserManager();
            var account = userManager.FindByUserName(request.AccountEmail);

            if(account is null)
            {
                throw new ArgumentException("User Account Does Not Exist");
            }

            if(account is Student)
            {
                var student = account as Student;
                var studentList = _searchService.SearchStudents(student.SchoolId, request.SearchInput);
                if (studentList is null)
                {
                    throw new ArgumentException("No Students Found");
                }
                return studentList;
            }

            throw new ArgumentException("Not authorized");
        }

        public List<SearchPersonDTO> SearchTeachers(SearchRequest request)
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

            if (account is Student)
            {
                var student = account as Student;
                var teacherList = _searchService.SearchTeachers(student.SchoolId, request.SearchInput);
                if (teacherList is null)
                {
                    throw new ArgumentException("No Teachers Found");
                }
                return teacherList;
            }

            throw new ArgumentException("Not authorized");
        }

        public List<Department> GetDepartments(SearchRequest request)
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

            if (account is Student)
            {
                var student = account as Student;
                var departmentList = _searchService.GetDepartments(student.SchoolId);
                if (departmentList is null || departmentList.Count == 0)
                {
                    throw new ArgumentException("No Departments Found");
                }
                return departmentList;
            }
            throw new ArgumentException("Not authorized");
        }
    }
}