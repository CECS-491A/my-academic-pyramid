using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.DTOs;
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

        public List<SearchDTO> SearchStudents(string searchName)
        {
            return _db.Students
                .Include("Department")
                .Where(s => s.SchoolId == _schoolId && (s.FirstName.Contains(searchName) || s.LastName.Contains(searchName)))
                .Select(s => new SearchDTO { Id = s.Id, FirstName = s.FirstName, LastName = s.LastName, Department = s.Department.Name})
                .ToList() ;

            //return _db.Students
            //    .Join(_db.Departments, s => s.DepartmentId, d => d.Id, (s, d) => new { Student = s, Department = d })
            //    .Where(sd => sd.Student.SchoolId == _schoolId &&
            //    (sd.Student.FirstName.Contains(searchName) || sd.Student.LastName.Contains(searchName)))
            //    .Select(sd => new SearchDTO { Id = sd.Student.Id, FirstName = sd.Student.FirstName, LastName = sd.Student.LastName, Department = sd.Department.Name })
            //    .ToList();
        }

        public List<SearchDTO> SearchTeachers(string searchName)
        {
            var teachers = (from teacher in _db.Teachers
                            from school in teacher.Schools
                            where school.Id == _schoolId && (teacher.FirstName.Contains(searchName)
                            || teacher.MiddleName.Contains(searchName) || teacher.LastName.Contains(searchName))
                            select new SearchDTO { Id = teacher.Id, FirstName = teacher.FirstName, MiddleName = teacher.MiddleName, LastName = teacher.LastName, Department = teacher.Department.Name })
                           .ToList();

            return teachers;
        }
    }
}