using DataAccessLayer;
using DataAccessLayer.DTOs.SchoolRegistrationDTO;
using DataAccessLayer.Models.School;
using Newtonsoft.Json.Linq;
using ServiceLayer.SchoolRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerLayer.Gateways.SchoolRegistration
{
    public class SchoolRegistrationManager
    {
        ISchoolRegistrationService schoolRegServ;
        public void SchoolRegister(SchoolDTO schoolDto,IEnumerable<DepartmentDTO> departmentDTOs, IEnumerable<CourseDTO> courseDTOs, IEnumerable<TeacherDTO> teacherDTOs )
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                schoolRegServ = new SchoolRegistrationService(db);
                var foundSchool = schoolRegServ.FindSchool(schoolDto.SchoolName);
                if(foundSchool == null)
                {
                    var newSchool = new School
                    {
                        Name = schoolDto.SchoolName,
                        ContactEmail = schoolDto.SchoolContactEmail,
                        EmailDomain = schoolDto.SchoolEmailDomain
                    };
                    foundSchool = schoolRegServ.CreateSchool(newSchool);
                }

                foreach(DepartmentDTO d in departmentDTOs)
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
                    if(foundSchoolDepartment == null)
                    {
                        var newSchoolDepartment = new SchoolDepartment
                        {
                            School = foundSchool,
                            Department = foundDepartment,
                        };
                        foundSchoolDepartment = schoolRegServ.CreateSchoolDepartment(newSchoolDepartment);

                    }


                    
                   

                    foreach (CourseDTO c in courseDTOs)
                    {
                        if(c.DepartmentName == foundDepartment.Name)
                        {
                            var foundCourse = schoolRegServ.FindCourse(c.CourseName);
                            if(foundCourse == null)
                            {
                                var newCourse = new Course
                                {
                                    Name = c.CourseName,
                                    SchoolDepartment = foundSchoolDepartment,
                                };
                                foundCourse = schoolRegServ.CreateCourse(newCourse);
                            }
                        }
                        db.SaveChanges();
                    }

                    foreach (TeacherDTO t in teacherDTOs)
                    {
                        var foundTeacher = schoolRegServ.FindTeacher(t.FirstName, t.LastName);
                        SchoolTeacher foundSchoolTeacher = null;
                        if (t.DepartmentName.Equals(foundDepartment.Name) )
                        {
                            
                            if (foundTeacher == null)
                            {
                                var newTeacher = new Teacher(t.FirstName, t.LastName);

                                foundTeacher = schoolRegServ.CreateTeacher(newTeacher);
                            }
                            var newSchoolTeacher = new SchoolTeacher
                            {
                                School = foundSchool,
                                Teacher = foundTeacher,
                                Department = foundSchoolDepartment
                            };

                            foundSchoolTeacher = schoolRegServ.CreateSchoolTeacher(newSchoolTeacher);

                        }             
                    }

                    //foreach (CourseDTO c in courseDTOs)
                    //{
                    //    var foundSchoolTeacher = db.SchoolTeachers.Where(st => st.School.Name.Equals(c.SchoolName) &&st.Teacher.FirstName.Equals(c.TeacherFirstName)
                    //                                                                  && st.Teacher.LastName.Equals(c.TeacherLastName)).FirstOrDefault();
                    //    var foundCourse = db.Courses.Where(sd => sd.Name.Equals(c.CourseName)).FirstOrDefault();

                    //    var newSchoolTeacherCourse = new SchoolTeacherCourse
                    //    {
                    //        SchoolTeacher = foundSchoolTeacher,
                    //        Course = foundCourse

                    //    };
                    //    schoolRegServ.CreateSchoolTeacherCourse(newSchoolTeacherCourse);
                    //}


                }
                db.SaveChanges();
            }      
            }
        
        }
    }

