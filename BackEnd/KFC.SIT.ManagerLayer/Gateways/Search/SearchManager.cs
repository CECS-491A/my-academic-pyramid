using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.DTOs;
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

        public SearchResultDTO SearchStudents(string searchInput)
        {
            if(searchInput is null)
            {
                throw new ArgumentNullException("Null Search Input"); // TODO: Make message constant
            }

            return (SearchResultDTO) _searchService.SearchStudents(searchInput);
        }

        public SearchResultDTO SearchTeachers(string searchInput)
        {
            throw new NotImplementedException();
        }
    }
}