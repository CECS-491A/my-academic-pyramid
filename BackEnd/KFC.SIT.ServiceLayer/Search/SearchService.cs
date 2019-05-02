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

        public SearchService(DatabaseContext db)
        {
            _db = db;
        }

        public List<SearchPersonDTO> SearchAllStudentsInSchool(int schoolId, string searchName)
        {
            return _db.Students
                .Where(s => s.SchoolId == schoolId && ((s.Account.FirstName + " " + s.Account.MiddleName + " " + s.Account.LastName).Contains(searchName) 
                || (s.Account.FirstName + " " + s.Account.LastName).Contains(searchName)))
                .Select(s => new SearchPersonDTO
                {
                    Id = s.Id,
                    FirstName = s.Account.FirstName,
                    MiddleName = s.Account.MiddleName,
                    LastName = s.Account.LastName,
                    Department = s.SchoolDepartment.Department.Name })
                .ToList();
        }

        public List<SearchPersonDTO> SearchAllTeachersInSchool(int schoolId, string searchName)
        {
            return _db.SchoolTeachers
                .Where(st => st.SchoolId == schoolId && (st.Teacher.FirstName + " " + st.Teacher.MiddleName + " " + st.Teacher.LastName).Contains(searchName)
                || (st.Teacher.FirstName + " " + st.Teacher.LastName).Contains(searchName))
                .Select(t => new SearchPersonDTO
                {
                    Id = t.TeacherId,
                    FirstName = t.Teacher.FirstName,
                    MiddleName = t.Teacher.MiddleName,
                    LastName = t.Teacher.LastName,
                    Department = t.Department.Department.Name,
                    Courses = t.Courses.Select(c => c.Course.Name).ToList()
                })
                .ToList();
        }

        public List<ForumPostDTO> SearchAllForumPostsInSchool(int schoolId, string searchName)
        {
            return _db.Questions
                .Where(q => q.SchoolId == schoolId && q.Text.Contains(searchName))
                .Select(q => new ForumPostDTO
                {
                    postId = q.Id,
                    title = (q.Account.FirstName + " " + q.Account.MiddleName + " " + q.Account.LastName),
                    headline = "" + q.DepartmentId,
                    subtitle = q.Text,
                    action = q.CreatedDate.ToString(),
                    //answers = q.Answers.Select(a => new ForumPostDTO
                    //{
                    //    postId = a.Id,
                    //    title = (a.Account.FirstName + " " + a.Account.MiddleName + " " + a.Account.LastName),
                    //    subtitle = a.Text,
                    //    action = a.CreatedDate.ToString(),
                    //}).ToList()
                }).ToList();

        }

        public List<SearchPersonDTO> SearchStudentsInDepartment(int schoolId, int departmentId, string searchName)
        {
            return _db.Students
                .Where(s => s.SchoolId == schoolId && s.DepartmentId == departmentId 
                && ((s.Account.FirstName + " " + s.Account.MiddleName + " " + s.Account.LastName)
                .Contains(searchName)
                || (s.Account.FirstName + " " + s.Account.LastName)
                .Contains(searchName)))
                .Select(s => new SearchPersonDTO
                {
                    Id = s.Id,
                    FirstName = s.Account.FirstName,
                    MiddleName = s.Account.MiddleName,
                    LastName = s.Account.LastName,
                    Department = s.SchoolDepartment.Department.Name
                })
                .ToList();
        }

        public List<SearchPersonDTO> SearchTeachersInDepartment(int schoolId, int departmentId, string searchName)
        {
            return _db.SchoolTeachers
                .Where(st => st.SchoolId == schoolId && st.DepartmentId == departmentId
                && (st.Teacher.FirstName + " " + st.Teacher.MiddleName + " " + st.Teacher.LastName)
                .Contains(searchName)
                || (st.Teacher.FirstName + " " + st.Teacher.LastName)
                .Contains(searchName))
                .Select(t => new SearchPersonDTO
                {
                    Id = t.TeacherId,
                    FirstName = t.Teacher.FirstName,
                    MiddleName = t.Teacher.MiddleName,
                    LastName = t.Teacher.LastName,
                    Department = t.Department.Department.Name,
                    Courses = t.Courses.Select(c => c.Course.Name).ToList()
                })
                .ToList();
        }

        public List<ForumPostDTO> SearchForumPostsInDepartment(int schoolId, int departmentId, string searchName)
        {
            return _db.Questions
                .Where(q => q.SchoolId == schoolId && q.DepartmentId == departmentId && q.Text.Contains(searchName))
                .Select(q => new ForumPostDTO
                {
                    postId = q.Id,
                    title = (q.Account.FirstName + " " + q.Account.MiddleName + " " + q.Account.LastName),
                    headline = "" + q.DepartmentId,
                    subtitle = q.Text,
                    action = q.CreatedDate.ToString(),
                    //answers = q.Answers.Select(a => new ForumPostDTO
                    //{
                    //    postId = a.Id,
                    //    title = (a.Account.FirstName + " " + a.Account.MiddleName + " " + a.Account.LastName),
                    //    subtitle = a.Text,
                    //    action = a.CreatedDate.ToString(),
                    //}).ToList()
                }).ToList();

        }

        public List<DepartmentDTO> GetDepartments(int schoolId)
        {
            return _db.SchoolDepartments
                .Where(sd => sd.SchoolId == schoolId)
                .Select(sd => new DepartmentDTO { id = sd.DepartmentID, text = sd.Department.Name, value = sd.DepartmentID })
                .OrderBy(sd => sd.text)
                .ToList();
        }

        public Student FindStudentByAccountId(int id)
        {
            return _db.Students
                .Where(s => s.AccountId == id)
                .FirstOrDefault();
        }
    }
}