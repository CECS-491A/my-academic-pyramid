using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using DataAccessLayer.Models.School;
using ManagerLayer.Gateways.Search;
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
    [Route("api/[controller]")]
    public class SearchController : ApiController
    {
        [HttpGet]
        [ActionName("students")]
        public IHttpActionResult SearchStudents([FromBody] SearchDTO request)
        {



            if (!ModelState.IsValid || request is null)
            {
                // 412 Response
                return Content(HttpStatusCode.PreconditionFailed, "NO WAY");
            }

            try
            {
                using (var _db = new DatabaseContext())
                {
                    ISearchManager manager = new SearchManager(_db, 1);
                    // Validate request and register application
                    return Content(HttpStatusCode.OK, manager.SearchStudents(request.SearchInput));
                }
            }
            catch (Exception x)
            {
                return Content(HttpStatusCode.BadRequest, "DIDNT WORK");
            }
        }
    }
}