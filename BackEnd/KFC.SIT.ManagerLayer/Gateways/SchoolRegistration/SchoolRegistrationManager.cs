using DataAccessLayer;
using DataAccessLayer.DTOs.SchoolRegistrationDTO;
using DataAccessLayer.Models.School;
using Newtonsoft.Json.Linq;
using ServiceLayer.SchoolRegistration;
using ServiceLayer.ServiceExceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ManagerLayer.Gateways.SchoolRegistration
{
    public class SchoolRegistrationManager
    {
        private ISchoolRegistrationService schoolRegServ;
        public void SchoolRegister(SchoolDTO schoolDto, IEnumerable<DepartmentDTO> departmentDTOs, IEnumerable<CourseDTO> courseDTOs, IEnumerable<TeacherDTO> teacherDTOs)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {


                        schoolRegServ = new SchoolRegistrationService(context);
                        var foundSchool = schoolRegServ.FindSchool(schoolDto.SchoolName);
                        if (foundSchool != null)
                        {
                            throw new SchoolRegistrationException();
                        }
                        if (foundSchool == null)
                        {
                            var newSchool = new School
                            {
                                Name = schoolDto.SchoolName,
                                ContactEmail = schoolDto.SchoolContactEmail,
                                EmailDomain = schoolDto.SchoolEmailDomain
                            };
                            foundSchool = schoolRegServ.CreateSchool(newSchool);
                        }

                        foreach (DepartmentDTO d in departmentDTOs)
                        {
                            var foundDepartment = schoolRegServ.FindDepartment(d.DepartmentName);
                            if (foundDepartment == null)
                            {
                                var newDepartment = new Department
                                {
                                    Name = d.DepartmentName
                                };
                                foundDepartment = schoolRegServ.CreateDepartment(newDepartment);

                            }

                            var foundSchoolDepartment = schoolRegServ.FindSchoolDepartment(foundSchool.Name, foundDepartment.Name);
                            if (foundSchoolDepartment == null)
                            {
                                var newSchoolDepartment = new SchoolDepartment
                                {
                                    School = foundSchool,
                                    Department = foundDepartment,
                                };
                                foundSchoolDepartment = schoolRegServ.CreateSchoolDepartment(newSchoolDepartment);

                            }

                            foreach (TeacherDTO t in teacherDTOs)
                            {
                                var foundTeacher = schoolRegServ.FindTeacher(t.FirstName, t.LastName);

                                if (t.DepartmentName.Equals(foundDepartment.Name))
                                {

                                    if (foundTeacher == null)
                                    {
                                        var newTeacher = new Teacher(t.FirstName, t.LastName);

                                        foundTeacher = schoolRegServ.CreateTeacher(newTeacher);
                                    }

                                    SchoolTeacher foundSchoolTeacher = schoolRegServ.FindSchoolTeacher(foundSchool.Name, foundDepartment.Name, foundTeacher.FirstName, foundTeacher.LastName);
                                    if (foundSchoolTeacher == null)
                                    {
                                        var newSchoolTeacher = new SchoolTeacher
                                        {

                                            Teacher = foundTeacher,
                                            SchoolDepartment = foundSchoolDepartment
                                        };
                                        foundSchoolTeacher = schoolRegServ.CreateSchoolTeacher(newSchoolTeacher);
                                    }

                                    foreach (CourseDTO c in courseDTOs)
                                    {
                                        if (c.DepartmentName.Equals(foundDepartment.Name) &&
                                            c.TeacherFirstName.Equals(foundTeacher.FirstName) &&
                                            c.TeacherLastName.Equals(foundTeacher.LastName))
                                        {

                                            var foundCourse = schoolRegServ.FindCourse(c.CourseName);
                                            if (foundCourse == null)
                                            {
                                                var newCourse = new Course
                                                {
                                                    Name = c.CourseName,
                                                    SchoolDepartment = foundSchoolDepartment

                                                };
                                                foundCourse = schoolRegServ.CreateCourse(newCourse);
                                                context.SaveChanges();

                                            }
                                            SchoolTeacherCourse foundSchoolTeacherCourse = schoolRegServ.FindSchoolTeacherCourse(foundSchool.Name, foundDepartment.Name, foundTeacher.FirstName, foundTeacher.LastName, foundCourse.Name);
                                            if (foundSchoolTeacherCourse == null)
                                            {
                                                var newSchoolTeacherCourse = new SchoolTeacherCourse
                                                {
                                                    SchoolTeacher = foundSchoolTeacher,
                                                    Course = foundCourse

                                                };
                                                schoolRegServ.CreateSchoolTeacherCourse(newSchoolTeacherCourse);
                                            }

                                        }
                                    }
                                }
                            }
                        }


                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error occurred.");
                    }
                }
            }
        }
    }
}

