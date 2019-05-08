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
using DataAccessLayer.DTOs.SearchDTO;

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
                .Include("Courses")
                .Where(predicate)
                .Select(s => new SearchPersonDTO
                {
                    AccountId = s.AccountId,
                    FirstName = s.Account.FirstName,
                    MiddleName = s.Account.MiddleName,
                    LastName = s.Account.LastName,
                    SchoolName = s.SchoolDepartment.School.Name,
                    DepartmentName = s.SchoolDepartment.Department.Name
                })
                .OrderBy(s => s.FirstName)
                .ToList();
        }

        public List<SearchPersonDTO> GetTeachers(Expression<Func<SchoolTeacher, bool>> predicate)
        {

            return _db.SchoolTeachers
                .Include("Courses")
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
                .OrderBy(t => t.LastName)
                .ToList();
        }

        public List<SearchForumPostDTO> GetSchoolQuestions(Expression<Func<SchoolQuestion,bool>> predicate)
        {
            return _db.Questions
                .OfType<PostedQuestion>()
                .OfType<SchoolQuestion>()
                .Where(predicate)
                .Select(q => new SearchForumPostDTO
                {
                    postId = q.Id,
                    title = (q.Account.FirstName + " " + q.Account.MiddleName + " " + q.Account.LastName),
                    headline = "" + q.School.Name,
                    subtitle = q.Text,
                    action = q.DateCreated.ToString(),
                    //answers = q.Answers.Select(a => new ForumPostDTO
                    //{
                    //    postId = a.Id,
                    //    title = (a.Account.FirstName + " " + a.Account.MiddleName + " " + a.Account.LastName),
                    //    subtitle = a.Text,
                    //    action = a.CreatedDate.ToString(),
                    //}).ToList()
                })
                .OrderBy(q => q.action)
                .ToList();
        }

        public List<SearchForumPostDTO> GetDepartmentQuestions(Expression<Func<DepartmentQuestion, bool>> predicate)
        {
            return _db.Questions
                .OfType<PostedQuestion>()
                .OfType<DepartmentQuestion>()
                .Where(predicate)
                .Select(q => new SearchForumPostDTO
                {
                    postId = q.Id,
                    title = (q.Account.FirstName + " " + q.Account.MiddleName + " " + q.Account.LastName),
                    headline = "" + q.SchoolDepartment.School.Name + " > " + q.SchoolDepartment.Department.Name,
                    subtitle = q.Text,
                    action = q.DateCreated.ToString(),
                    //answers = q.Answers.Select(a => new ForumPostDTO
                    //{
                    //    postId = a.Id,
                    //    title = (a.Account.FirstName + " " + a.Account.MiddleName + " " + a.Account.LastName),
                    //    subtitle = a.Text,
                    //    action = a.CreatedDate.ToString(),
                    //}).ToList()
                })
                .OrderBy(q => q.action)
                .ToList();
        }

        public List<SearchForumPostDTO> GetCourseQuestions(Expression<Func<CourseQuestion, bool>> predicate)
        {
            return _db.Questions
                .OfType<PostedQuestion>()
                .OfType<CourseQuestion>()
                .Where(predicate)
                .Select(q => new SearchForumPostDTO
                {
                    postId = q.Id,
                    title = (q.Account.FirstName + " " + q.Account.MiddleName + " " + q.Account.LastName),
                    headline = "" + q.Course.SchoolDepartment.School.Name + " > " + q.Course.SchoolDepartment.Department.Name + " > " + q.Course.Name,
                    subtitle = q.Text,
                    action = q.DateCreated.ToString(),
                    //answers = q.Answers.Select(a => new ForumPostDTO
                    //{
                    //    postId = a.Id,
                    //    title = (a.Account.FirstName + " " + a.Account.MiddleName + " " + a.Account.LastName),
                    //    subtitle = a.Text,
                    //    action = a.CreatedDate.ToString(),
                    //}).ToList()
                })
                .OrderBy(q => q.action)
                .ToList();
        }

        public List<SearchFilterSelectionDTO> GetSchools()
        {
            return _db.Schools
                .Select(s => new SearchFilterSelectionDTO { id = s.Id, text = s.Name, value = s.Id })
                .OrderBy(s => s.text)
                .ToList();
        }

        public List<SearchFilterSelectionDTO> GetDepartments(int schoolId)
        {
            return _db.SchoolDepartments
                .Where(sd => sd.SchoolId == schoolId)
                .Select(sd => new SearchFilterSelectionDTO { id = sd.DepartmentID, text = sd.Department.Name, value = sd.DepartmentID })
                .OrderBy(sd => sd.text)
                .ToList();
        }

        public List<SearchFilterSelectionDTO> GetCourses(int schoolId, int departmentId)
        {
            return _db.SchoolTeacherCourses
                .Where(c => c.SchoolTeacher.SchoolDepartment.SchoolId == schoolId && c.SchoolTeacher.SchoolDepartment.DepartmentID == departmentId)
                .Select(c => new SearchFilterSelectionDTO { id = c.CourseId, text = c.Course.Name, value = c.CourseId })
                .OrderBy(c => c.text)
                .ToList();
        }

        public Course GetCourse(int courseId)
        {
            return _db.Courses.Find(courseId);
        }

        public Student FindStudentByAccountId(int id)
        {
            return _db.Students
                .Where(s => s.AccountId == id)
                .FirstOrDefault();
        }
   }
}