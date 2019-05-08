﻿using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.DiscussionForum;
using DataAccessLayer.Models.Requests;
using DataAccessLayer.Models.School;
using ManagerLayer.Gateways.Search;
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

        [HttpGet]
        [ActionName("input")]
        public IHttpActionResult Search([FromUri] SearchRequest request)
        {
            if (!ModelState.IsValid || request is null)
            {
                // 412 Response
                return Content(HttpStatusCode.PreconditionFailed, new SearchResponse("Invalid Request"));
            }

           try
           {
               using (var _db = new DatabaseContext())
               {
                    ISearchManager manager = new SearchManager(_db);

                    var results = new SearchResponse(manager.Search(request));
                    return Content(HttpStatusCode.OK, results);
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

        [HttpGet]
        [ActionName("account")]
        public IHttpActionResult GetAccount([FromUri] int AccountId)
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

                    var results = manager.GetAccount(AccountId);
                    return Content(HttpStatusCode.OK, results);
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
                        case 0:
                            return Content(HttpStatusCode.OK, manager.GetSchools());
                        case 1:
                            return Content(HttpStatusCode.OK, manager.GetDepartments(request.SearchSchool));
                        case 2:
                            return Content(HttpStatusCode.OK, manager.GetCourses(request.SearchSchool, request.SearchDepartment));
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