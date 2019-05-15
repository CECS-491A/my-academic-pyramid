﻿using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.DiscussionForum;
using DataAccessLayer.Models.Requests;
using DataAccessLayer.Models.School;
using KFC.SIT.WebAPI.Utility;
using ManagerLayer.Gateways.Logging;
using ManagerLayer.Gateways.Search;
using SecurityLayer.Authorization;
using SecurityLayer.Sessions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
// TODO: Make text constants
namespace KFC.SIT.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : ApiController
    {

        private LoggingManager _loggingManager = new LoggingManager();

        [HttpGet]
        [ActionName("input")]
        public IHttpActionResult Search([FromUri] SearchRequest request)
        {
            if (!ModelState.IsValid || request is null)
            {
                // 412 Response
                return Content(HttpStatusCode.PreconditionFailed, new SearchResponse(Constants.InvalidRequest));
            }

            // Validate token and session
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
            );
            if (securityContext == null)
            {
                _loggingManager.LogError(securityContext.UserName, HttpContext.Current.Request.ToString(), Constants.InvalidSecurityContext);
                return Content(HttpStatusCode.Unauthorized, Constants.InvalidSecurityContext);
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                _loggingManager.LogError(securityContext.UserName, HttpContext.Current.Request.ToString(), Constants.InvalidSession);
                return Content(HttpStatusCode.Unauthorized, Constants.InvalidSession);
            }

            // Update token
            string updatedToken = sm.RefreshSession(securityContext.Token);

            try
            {
               using (var _db = new DatabaseContext())
               {
                    ISearchManager manager = new SearchManager(_db);

                    // Search
                    var results = new SearchResponse(manager.Search(request), updatedToken);
                    return Content(HttpStatusCode.OK, results);
                }
            }
            catch (Exception x) when (x is ArgumentException)
            {
                _loggingManager.LogError(securityContext.UserName, HttpContext.Current.Request.ToString(), x.Message);
                return Content(HttpStatusCode.BadRequest, new SearchResponse(x.Message));
            }
            catch (Exception x)
            {
                _loggingManager.LogError(securityContext.UserName, HttpContext.Current.Request.ToString(), x.Message);
                return Content(HttpStatusCode.InternalServerError, new SearchResponse(x.Message));
            }
        }
        

        [HttpGet]
        [ActionName("selections")]
        public IHttpActionResult GetSearchSelections([FromUri] SearchRequest request)
        {
            if (!ModelState.IsValid)
            {
                // 412 Response
                return Content(HttpStatusCode.PreconditionFailed, new SearchResponse("Invalid Request"));
            }

            try
            {
                using (var _db = new DatabaseContext())
                {
                    ISearchManager manager = new SearchManager(_db);
                    switch (request.SearchCategory)
                    {
                        // Get all schools
                        case 0:
                            return Content(HttpStatusCode.OK, manager.GetSchools());
                        // Get all departments in a school
                        case 1:
                            return Content(HttpStatusCode.OK, manager.GetDepartments(request.SearchSchool));
                        // Get all courses in a department
                        case 2:
                            return Content(HttpStatusCode.OK, manager.GetCourses(request.SearchSchool, request.SearchDepartment));
                        case 3:
                            return Content(HttpStatusCode.OK, manager.GetTeachers(request.SearchSchool, request.SearchDepartment, request.SearchCourse));
                    }
                    throw new ArgumentException("Invalid Search Selection Request");
                }
            }
            catch (Exception x) when (x is ArgumentException)
            {
                return Content(HttpStatusCode.BadRequest, new SearchResponse(x.Message));
            }
            catch (Exception x)
            {
                return Content(HttpStatusCode.InternalServerError, new SearchResponse(x.Message));
            }
        }
    }
}