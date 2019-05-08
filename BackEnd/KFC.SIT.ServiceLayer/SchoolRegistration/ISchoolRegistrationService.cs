﻿using System;
using DataAccessLayer.Models.School;

namespace ServiceLayer.SchoolRegistration
{
    public interface ISchoolRegistrationService
    {
        Course CreateCourse(Course course);
        Department CreateDepartment(Department department);
        School CreateSchool(School school);
        SchoolDepartment CreateSchoolDepartment(SchoolDepartment schoolDepartment);
        SchoolTeacher CreateSchoolTeacher(SchoolTeacher schoolTeacher);
        SchoolTeacherCourse CreateSchoolTeacherCourse(SchoolTeacherCourse schoolTeacherCourse);
        Teacher CreateTeacher(Teacher teacher);
        Course FindCourse(string courseName);
        Department FindDepartment(string departmentName);
        SchoolDepartment FindSchoolDepartment(string schoolName, string departmentName);
        SchoolTeacher FindSchoolTeacher(string schoolName, string departmentName, string teacherFName, string teacherLName);
        School FindSchool(string schoolName);
        Teacher FindTeacher(string teacherFirstName, string teacherLastname);
    }
}