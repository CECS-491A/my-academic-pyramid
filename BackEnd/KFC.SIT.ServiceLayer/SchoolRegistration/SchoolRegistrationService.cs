
using DataAccessLayer;
using DataAccessLayer.Models.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.SchoolRegistration
{
    public class SchoolRegistrationService : ISchoolRegistrationService
    {
        DatabaseContext _DbContext;

        public SchoolRegistrationService(DatabaseContext DbContext)
        {
            _DbContext = DbContext;
        }

        public School CreateSchool(School school)
        {
            return _DbContext.Schools.Add(school);
        }

        public Department CreateDepartment(Department department)
        {
            return _DbContext.Departments.Add(department);
        }

        public Course CreateCourse(Course course)
        {
            return _DbContext.Courses.Add(course);
        }

        public Teacher CreateTeacher (Teacher teacher)
        {
            return _DbContext.Teachers.Add(teacher);
        }


        public SchoolDepartment CreateSchoolDepartment(SchoolDepartment schoolDepartment)
        {
            return _DbContext.SchoolDepartments.Add(schoolDepartment);
        }

        public SchoolTeacher CreateSchoolTeacher(SchoolTeacher schoolTeacher)
        {
            return _DbContext.SchoolTeachers.Add(schoolTeacher);
        }

        public SchoolTeacherCourse CreateSchoolTeacherCourse(SchoolTeacherCourse schoolTeacherCourse)
        {
            return _DbContext.SchoolTeacherCourses.Add(schoolTeacherCourse);
        }

        public School FindSchool(int schoolId)
        {
            return _DbContext.Schools.Find(schoolId);
        }

        public School FindSchool (String schoolName)
        {
            return _DbContext.Schools.Where(s => s.Name.Equals(schoolName)).FirstOrDefault();
        }

        public Department FindDepartment(String departmentName)
        {
            return _DbContext.Departments.Where(s => s.Name.Equals(departmentName)).FirstOrDefault();
        }

        public SchoolDepartment FindSchoolDepartment(int schoolId, int departmentId)
        {
            return _DbContext.SchoolDepartments.Where(sd => sd.SchoolId == schoolId
                                                        && sd.DepartmentID == departmentId).FirstOrDefault();
        }

        public SchoolDepartment FindSchoolDepartment(string schoolName, string departmentName)
        {
            return _DbContext.SchoolDepartments.Where(sd => sd.School.Name.Equals(schoolName)
                                                       && sd.Department.Name.Equals(departmentName)).FirstOrDefault();
        }

        public SchoolTeacher FindSchoolTeacher(string schoolName, string departmentName, string teacherFName, string teacherLName)
        {
            return _DbContext.SchoolTeachers.Where(sd => sd.SchoolDepartment.School.Name.Equals(schoolName)
                                                       && sd.SchoolDepartment.Department.Name.Equals(departmentName)
                                                       && sd.Teacher.FirstName.Equals(teacherFName)
                                                        && sd.Teacher.LastName.Equals(teacherLName)
                                                       ).FirstOrDefault();
        }

        public SchoolTeacherCourse FindSchoolTeacherCourse(string schoolName, string departmentName, string teacherFName, string teacherLName, string courseName)
        {
            return _DbContext.SchoolTeacherCourses.Where(sd => sd.SchoolTeacher.SchoolDepartment.School.Name.Equals(schoolName)
                                                       && sd.SchoolTeacher.SchoolDepartment.Department.Name.Equals(departmentName)
                                                       && sd.SchoolTeacher.Teacher.FirstName.Equals(teacherFName)
                                                        && sd.SchoolTeacher.Teacher.FirstName.Equals(teacherLName)
                                                        && sd.Course.Name.Equals(courseName)
                                                       ).FirstOrDefault();
        }


        public Course FindCourse (String courseName)
        {
            return _DbContext.Courses.Where(s => s.Name.Equals(courseName)).FirstOrDefault();

        }

        public Teacher FindTeacher(String teacherFirstName, String teacherLastname)
        {
            return _DbContext.Teachers.Where(t => t.FirstName.Equals(teacherFirstName) && t.LastName.Equals(teacherLastname)
                                                                ).FirstOrDefault();
        }

        


    }
}