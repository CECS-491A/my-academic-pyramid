using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models.School;
using System.Data;
using System.Linq.Expressions;
using DataAccessLayer.Models.DiscussionForum;

namespace ServiceLayer.Search
{
   public class SearchService : ISearchService
   {
       private DatabaseContext _db;

       public SearchService(DatabaseContext db)
       {
           _db = db;
       }

        public List<SearchPersonDTO> GetStudents(Expression<Func<Student, bool>> predicate)
        {
            return _db.Students
                .Where(predicate)
                .Select(s => new SearchPersonDTO
                {
                    AccountId = s.Id,
                    FirstName = s.Account.FirstName,
                    MiddleName = s.Account.MiddleName,
                    LastName = s.Account.LastName,
                    SchoolName = s.SchoolDepartment.School.Name,
                    DepartmentName = s.SchoolDepartment.Department.Name
                })
                .ToList();
        }

        public List<SearchPersonDTO> GetTeachers(Expression<Func<SchoolTeacher, bool>> predicate)
        {

            return _db.SchoolTeachers
                .Where(predicate)
                .Select(t => new SearchPersonDTO
                {
                    AccountId = t.TeacherId,
                    FirstName = t.Teacher.FirstName,
                    MiddleName = t.Teacher.MiddleName,
                    LastName = t.Teacher.LastName,
                    SchoolName = t.SchoolDepartment.School.Name,
                    DepartmentName = t.SchoolDepartment.Department.Name,
                    Courses = t.Courses.Select(c => c.Course.Name).ToList()
                })
                .ToList();
        }

        public List<SearchForumPostDTO> GetForumPosts(Expression<Func<Question, bool>> predicate)
        {
            return _db.Questions
                .Where(predicate)
                .Select(q => new SearchForumPostDTO
                {
                    postId = q.Id,
                    title = (q.Account.FirstName + " " + q.Account.MiddleName + " " + q.Account.LastName),
                    //headline = "" + q.Department.Department.Name,
                    subtitle = q.Text,
                    //action = q.CreatedDate.ToString(),
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