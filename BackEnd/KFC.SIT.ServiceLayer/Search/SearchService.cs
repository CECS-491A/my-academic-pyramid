using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.Models.School;

namespace ServiceLayer.Search
{
    public class SearchService : ISearchService
    {
        private DatabaseContext _db;
        private readonly int _schoolId;

        public SearchService(DatabaseContext db, int schoolId)
        {
            _db = db;
            _schoolId = schoolId;
        }

        public IQueryable SearchStudents(string searchName)
        {
            throw new NotImplementedException();
        }

        public IQueryable SearchTeachers(string searchName)
        {
            throw new NotImplementedException();
        }
    }
}