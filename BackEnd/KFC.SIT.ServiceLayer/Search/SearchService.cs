﻿using System;
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

        /// <summary>
        /// Get a list of students
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
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
                    DepartmentName = s.SchoolDepartment.Department.Name,
                    Courses = s.Courses.Select(c => c.Course.Name).ToList()
                })
                .OrderBy(s => s.FirstName)
                .ToList();
        }

        /// <summary>
        /// Get a list of teachers
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a list of school questions
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a list of department questions
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a list of course questions
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get a list of all schools
        /// </summary>
        /// <returns></returns>
        public List<SearchFilterSelectionDTO> GetSchools()
        {
            return _db.Schools
                .Select(s => new SearchFilterSelectionDTO { id = s.Id, text = s.Name, value = s.Id })
                .OrderBy(s => s.text)
                .ToList();
        }

        /// <summary>
        /// Get a list of all departments in a school
        /// </summary>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        public List<SearchFilterSelectionDTO> GetDepartments(int schoolId)
        {
            return _db.SchoolDepartments
                .Where(sd => sd.SchoolId == schoolId)
                .Select(sd => new SearchFilterSelectionDTO { id = sd.Id, text = sd.Department.Name, value = sd.Id })
                .OrderBy(sd => sd.text)
                .ToList();
        }

        /// <summary>
        /// Get a list of all courses in a department in a school
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<SearchFilterSelectionDTO> GetCourses(int schoolId, int departmentId)
        {
            return _db.SchoolTeacherCourses
                .Where(c => c.SchoolTeacher.SchoolDepartment.SchoolId == schoolId && c.SchoolTeacher.SchoolDepartment.DepartmentID == departmentId)
                .GroupBy(c => c.CourseId)
                .Select(g => g.FirstOrDefault())
                .Select(cf => new SearchFilterSelectionDTO { id = cf.CourseId, text = cf.Course.Name, value = cf.CourseId })
                .OrderBy(c => c.text)
                .ToList();
        }

        public List<SearchFilterSelectionDTO> GetSchoolTeacherCourses(int schoolId, int departmentId, int courseId)
        {
            return _db.SchoolTeacherCourses
                .Where(stc => stc.SchoolTeacher.SchoolDepartment.SchoolId == schoolId
                        && stc.SchoolTeacher.SchoolDepartment.DepartmentID == departmentId
                        && stc.CourseId == courseId)
                .GroupBy(stc => new { stc.SchoolTeacherId, stc.CourseId })
                .Select(stc => stc.FirstOrDefault())
                .Select(stc => new SearchFilterSelectionDTO { id = stc.Id,
                                                              text = (stc.Course.Name + " Professor: " 
                                                                      + stc.SchoolTeacher.Teacher.LastName + ", " 
                                                                      + stc.SchoolTeacher.Teacher.FirstName),
                                                              value = stc.Id })
                .OrderBy(c => c.text)
                .ToList();
        }

        /// <summary>
        /// Get a list of all courses in a department in a school
        /// </summary>
        /// <param name="schoolId"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<SearchFilterSelectionDTO> GetCourses(int schoolDepartmentId)
        {
            return _db.Courses
                .Where(c => c.SchoolDeparmentId == schoolDepartmentId)
                .Select(c => new SearchFilterSelectionDTO { id = c.Id, text = c.Name, value = c.Id })
                .OrderBy(c => c.text)
                .ToList();
        }

        /// <summary>
        /// Get a course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public Course GetCourse(int courseId)
        {
            return _db.Courses.Find(courseId);
        }
   }
}