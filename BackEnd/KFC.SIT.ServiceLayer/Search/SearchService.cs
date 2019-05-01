using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models.School;

namespace ServiceLayer.Search
{
    public class SearchService //: ISearchService
    {
        private DatabaseContext _db;

        public SearchService(DatabaseContext db)
        {
            _db = db;
        }

        //public List<SearchPersonDTO> SearchStudents(int schoolId, string searchName)
        //{
            

        //    return _db.Students
        //        .Include("Department")
        //        .Where(s => s.SchoolId == schoolId && ((s.FirstName + " " + s.MiddleName + " " + s.LastName).Contains(searchName) || (s.FirstName + " " + s.LastName).Contains(searchName)))
        //        .Select(s => new SearchPersonDTO { Id = s.Id, FirstName = s.FirstName, MiddleName = s.MiddleName, LastName = s.LastName, Department = s.Department.Name})
        //        .ToList();

        //    //return _db.Students
        //    //    .Join(_db.Departments, s => s.DepartmentId, d => d.Id, (s, d) => new { Student = s, Department = d })
        //    //    .Where(sd => sd.Student.SchoolId == _schoolId &&
        //    //    (sd.Student.FirstName.Contains(searchName) || sd.Student.LastName.Contains(searchName)))
        //    //    .Select(sd => new SearchDTO { Id = sd.Student.Id, FirstName = sd.Student.FirstName, LastName = sd.Student.LastName, Department = sd.Department.Name })
        //    //    .ToList();
        //}

        //public List<SearchPersonDTO> SearchTeachers(int schoolId, string searchName)
        //{
        //    var teachers = _db.Schools
        //        .Where(s => s.Id == schoolId)
        //        .SelectMany(s => s.Teachers);

        //    return teachers
        //        .Where(t => (t.FirstName + " " + t.MiddleName + " " + t.LastName).Contains(searchName) || (t.FirstName + " " + t.LastName).Contains(searchName))
        //        .Select(t => new SearchPersonDTO { Id = t.Id, FirstName = t.FirstName, MiddleName = t.MiddleName, LastName = t.LastName, Department = t.Department.Name, Courses = t.Courses.Select(c => c.Name).ToList()})
        //        .ToList();


        //    //var teachers = (from teacher in _db.Teachers
        //    //                from school in teacher.Schools
        //    //                where school.Id == schoolId && ((teacher.FirstName + " " + teacher.MiddleName + " " + teacher.LastName).Contains(searchName) || (teacher.FirstName + " " + teacher.LastName).Contains(searchName))
        //    //                select new SearchPersonDTO { Id = teacher.Id, FirstName = teacher.FirstName, MiddleName = teacher.MiddleName, LastName = teacher.LastName, Department = teacher.Department.Name })
        //    //               .ToList();

        //    //return teachers;
        //}

        //public List<Department> GetDepartments(int schoolId)
        //{
        //    return _db.Schools
        //        .Where(s => s.Id == schoolId)
        //        .SelectMany(s => s.Departments)
        //        .ToList();

        //    //return (from school in _db.Schools
        //    //        from department in school.Departments
        //    //        where school.Id == schoolId
        //    //        select department)
        //    //        .ToList();
        //}
    }
}