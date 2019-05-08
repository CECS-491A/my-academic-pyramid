using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SecurityLayer.Sessions;
using SecurityLayer;
using SecurityLayer.Authorization.AuthorizationManagers;
using Newtonsoft.Json;
using SecurityLayer.Authorization;
using DataAccessLayer;
using WebAPI.Gateways.UserManagement;
using System.Web.Http.Cors;
using DataAccessLayer.Models;
using KFC.SIT.WebAPI.Utility;
using DataAccessLayer.Models.School;
using ServiceLayer.SchoolRegistration;

namespace KFC.SIT.WebAPI.Controllers.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RegistrationController : ApiController
    {

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Post(RegistrationData registrationData)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
                Request.Headers
            );
            if (securityContext == null)
            {
                return Unauthorized();
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Unauthorized();
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanRegister"
            };
            if (!authorizationManager.CheckClaims(requiredClaims))
            {
                return Unauthorized();
            }
            else
            {
                UserManager um = new UserManager();
                Account user = um.FindByUserName(securityContext.UserName);
                if (user == null)
                {
                    return NotFound();
                }
                user.FirstName = registrationData.FirstName;
                user.MiddleName = registrationData.MiddleName;
                user.LastName = registrationData.LastName;

                // User is a student
                if (registrationData.SchoolId > 0)
                {
                    using (var _db = new DatabaseContext())
                    {
                        ISchoolRegistrationService srs = new SchoolRegistrationService(_db);
                        var school = srs.FindSchool(registrationData.SchoolId);
                        var domainIndex = user.UserName.IndexOf('@');
                        if (school.EmailDomain.Equals(user.UserName.Substring(domainIndex + 1))){
                            var schoolDepartment = srs.FindSchoolDepartment(registrationData.SchoolId, registrationData.DepartmentId);
                            Student student = new Student(user.Id, schoolDepartment.Id);
                            user.Students.Add(student);
                        }
                        else
                        {
                            return BadRequest("User's Email Does Not Match School's Email Domain");
                        }
                    }
                }

                // TODO test this.
                user.DateOfBirth = registrationData.DateOfBirth;
                um.UpdateUserAccount(user);
                um.SetCategory(user.Id, "Student");
                um.RemoveClaimAction(user.Id, "CanRegister");
                um.AutomaticClaimAssigning(user);
                string updatedToken = sm.RefreshSessionUpdatedPayload(
                    securityContext.Token,
                    securityContext.UserId
                );
                Dictionary<string, string> responseContent = new Dictionary<string, string>()
                {
                    { "SITtoken", updatedToken}
                };
                return Ok(responseContent);
            }
        }

    }
}