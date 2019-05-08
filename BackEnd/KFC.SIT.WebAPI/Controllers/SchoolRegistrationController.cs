using DataAccessLayer.DTOs.SchoolRegistrationDTO;
using ManagerLayer.Gateways.SchoolRegistration;
using Newtonsoft.Json.Linq;
using ServiceLayer.ServiceExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace KFC.SIT.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SchoolRegistrationController :ApiController
    {
        [HttpPost]
        public IHttpActionResult PostSchoolData([FromBody] JObject data)
        {
            SchoolDTO schoolDTO = data["schoolDTO"].ToObject<SchoolDTO>();
            IEnumerable<DepartmentDTO> departmentDTOs = data["departmentDTO"].ToObject<IEnumerable<DepartmentDTO>>();
            IEnumerable<CourseDTO> courseDTOs = data["CourseDTO"].ToObject<IEnumerable<CourseDTO>>();
            IEnumerable<TeacherDTO> teacherDTOs = data["TeacherDTO"].ToObject<IEnumerable<TeacherDTO>>();
            SchoolRegistrationManager srm = new SchoolRegistrationManager();
            try
            {
                srm.SchoolRegister(schoolDTO, departmentDTOs, courseDTOs, teacherDTOs);
                return Ok();
            }

            catch(SchoolRegistrationException ex)
            {
                return Content(HttpStatusCode.Conflict, "School with same name exists");
            }
            
        }
    }
}