using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models.School;
using ServiceLayer.Search;

namespace ManagerLayer.Gateways.Search
{
    public class SearchManager : ISearchManager
    {

        private DatabaseContext _db;
        private readonly int _schoolId;
        private ISearchService _searchService;

        public SearchManager(DatabaseContext db, int schoolId)
        {
            _db = db;
            _schoolId = schoolId;
            _searchService = new SearchService(_db, _schoolId);
        }

        // TODO: Make all messages constants
        public List<SearchDTO> SearchStudents(string searchInput)
        {
            if (searchInput is null)
            {
                throw new ArgumentNullException("Search Input is Null");
            }

            var students = _searchService.SearchStudents(searchInput);
            if (students is null)
            {
                throw new ArgumentException("No Students Found");
            }

            return students;
        }

        public List<SearchDTO> SearchTeachers(string searchInput)
        {
            if(searchInput is null)
            {
                throw new ArgumentNullException("Search Input is Null");
            }

            var teachers = _searchService.SearchTeachers(searchInput);
            if(teachers is null)
            {
                throw new ArgumentException("No Teachers Found");
            }

            return teachers;
        }
    }
}