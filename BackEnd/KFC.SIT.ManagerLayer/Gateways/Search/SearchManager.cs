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

    }
}