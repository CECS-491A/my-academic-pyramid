using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ManagerLayer.Controllers
{
    [EnableCors(origins: "https://myacademicpyramid.com", headers: "*", methods: "*")]
    public class HomeController : ApiController
    {
        [HttpGet]
        public String Hello()
        {
            return "Hello World From Backend API";
        }
    }
}
