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
            return _db.Students
                .Join(_db.Departments, s => s.DepartmentId, d => d.Id, (s, d) => new { Student = s, Department = d })
                .Where(sd => sd.Student.SchoolId == _schoolId &&
                (sd.Student.FirstName == searchName || sd.Student.LastName == searchName))
                .Select(sd => new { sd.Student.Id, sd.Student.FirstName, sd.Student.LastName, sd.Department.Name });
        }

        public IQueryable SearchTeachers(string searchName)
        {

            var teachers = (from schoolTeacher in _db.SchoolTeachers
                            join teacher in _db.Teachers
                            on schoolTeacher.TeacherId equals teacher.Id
                            join schoolDepartment in _db.SchoolDepartments
                            on schoolTeacher.SchoolDepartmentId equals schoolDepartment.Id
                            join department in _db.Departments
                            on schoolDepartment.DepartmentId equals department.Id
                            where schoolTeacher.Id == _schoolId && (teacher.FirstName == searchName
                            || teacher.MiddleName == searchName || teacher.LastName == searchName)
                            select new { teacher.Id, teacher.FirstName, teacher.MiddleName, teacher.LastName, department.Name });

            return teachers;
        }
    }
}