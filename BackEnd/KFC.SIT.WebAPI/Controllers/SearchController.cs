//using DataAccessLayer;
//using DataAccessLayer.DTOs;
//using DataAccessLayer.Models;
//using DataAccessLayer.Models.Requests;
//using DataAccessLayer.Models.School;
//using ManagerLayer.Gateways.Search;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Http;
//using System.Web.Http.Cors;
//// TODO: Make text constants
//namespace KFC.SIT.WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    public class SearchController : ApiController
//    {
//        [HttpGet]
//        [ActionName("input")]
//        public IHttpActionResult SearchStudents([FromUri] SearchRequest request)
//        {
//            if (!ModelState.IsValid || request is null)
//            {
//                // 412 Response
//                return Content(HttpStatusCode.PreconditionFailed, new SearchResponse("Invalid Request"));
//            }

//            try
//            {
//                using (var _db = new DatabaseContext())
//                {
//                    ISearchManager manager = new SearchManager(_db);

//                    switch (request.SearchCategory)
//                    {
//                        case 0:
//                            var students = new SearchResponse(manager.Search(request,request.SearchCategory));
//                            return Content(HttpStatusCode.OK, students);
//                        case 1:
//                            var teachers = new SearchResponse(manager.Search(request,request.SearchCategory));
//                            return Content(HttpStatusCode.OK, teachers);
//                        case 2:
//                            var posts = new SearchResponse(manager.Search(request,request.SearchCategory));
//                            return Content(HttpStatusCode.OK, posts);
//                        default:
//                            throw new ArgumentException("Invalid Search Category");
//                    }
//                }
//            }
//            catch (Exception x) when (x is ArgumentException)
//            {
//                return Content(HttpStatusCode.BadRequest, new SearchResponse(x.Message));
//            }
//            catch (Exception x)
//            {
//                return Content(HttpStatusCode.InternalServerError, new SearchResponse(x.Message));
//            }
//        }
        

//        [HttpGet]
//        [ActionName("departments")]
//        public IHttpActionResult GetDepartments([FromUri] int AccountId)
//        {
//            if (!ModelState.IsValid)
//            {
//                // 412 Response
//                return Content(HttpStatusCode.PreconditionFailed, new SearchResponse("Invalid Request"));
//            }

//            try
//            {
//                using (var _db = new DatabaseContext())
//                {
//                    ISearchManager manager = new SearchManager(_db);
//                    return Content(HttpStatusCode.OK, manager.GetDepartments(AccountId));
//                }
//            }
//            catch (Exception x) when (x is ArgumentException)
//            {
//                return Content(HttpStatusCode.BadRequest, new SearchResponse(x.Message));
//            }
//            catch (Exception x)
//            {
//                return Content(HttpStatusCode.InternalServerError, new SearchResponse(x.Message));
//            }
//        }
//    }
//}