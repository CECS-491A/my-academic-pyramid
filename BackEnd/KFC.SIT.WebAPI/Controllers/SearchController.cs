using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Requests;
using DataAccessLayer.Models.School;
using ManagerLayer.Gateways.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
// TODO: Make text constants
namespace KFC.SIT.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SearchController : ApiController
    {
        [HttpGet]
        [ActionName("students")]
        public IHttpActionResult SearchStudents([FromUri] SearchRequest request)
        {
            if (!ModelState.IsValid || request is null)
            {
                // 412 Response
                return Content(HttpStatusCode.PreconditionFailed, "Invalid Request");
            }

            try
            {
                using (var _db = new DatabaseContext())
                {
                    ISearchManager manager = new SearchManager(_db);
                    SearchResponse response = new SearchResponse(manager.SearchStudents(request));
                    return Content(HttpStatusCode.OK, response);
                }
            }
            catch (Exception x) when (x is ArgumentException)
            {
                return Content(HttpStatusCode.BadRequest, x.Message);
            }
            catch (Exception x)
            {
                return Content(HttpStatusCode.InternalServerError, x.Message);
            }
        }

        [HttpGet]
        [ActionName("teachers")]
        public IHttpActionResult SearchTeachers([FromUri] SearchRequest request)
        {
            if (!ModelState.IsValid || request is null)
            {
                // 412 Response
                return Content(HttpStatusCode.PreconditionFailed, "Invalid Request");
            }

            try
            {
                using (var _db = new DatabaseContext())
                {
                    ISearchManager manager = new SearchManager(_db);
                    SearchResponse response = new SearchResponse(manager.SearchTeachers(request));
                    return Content(HttpStatusCode.OK, response);
                }
            }
            catch (Exception x) when (x is ArgumentException)
            {
                return Content(HttpStatusCode.BadRequest, x.Message);
            }
            catch (Exception x)
            {
                return Content(HttpStatusCode.InternalServerError, x.Message);
            }
        }

        [HttpGet]
        [ActionName("departments")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult GetDepartments([FromUri] SearchRequest request)
        {
            if (!ModelState.IsValid || request is null)
            {
                // 412 Response
                return Content(HttpStatusCode.PreconditionFailed, "Invalid Request");
            }

            try
            {
                using (var _db = new DatabaseContext())
                {
                    ISearchManager manager = new SearchManager(_db);
                    SearchResponse response = new SearchResponse(manager.GetDepartments(request));
                    return Content(HttpStatusCode.OK, response);
                }
            }
            catch (Exception x) when (x is ArgumentException)
            {
                return Content(HttpStatusCode.BadRequest, x.Message);
            }
            catch (Exception x)
            {
                return Content(HttpStatusCode.InternalServerError, x.Message);
            }
        }
    }
}