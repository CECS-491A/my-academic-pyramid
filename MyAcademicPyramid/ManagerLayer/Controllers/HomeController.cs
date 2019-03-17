using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using SecurityLayer;

namespace ManagerLayer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : ApiController
    {
        [HttpGet]
        public String Hello()
        {
            return "Hello World From Backend API " + DateTime.Now;
        }

        [HttpGet]
        [Route("Home/TestJWT")]
        public string TestJWT()
        {
            Dictionary<string, string> testDict = new Dictionary<string, string>()
            {
                {"User", "ljulian2190@gmail.com" },
                {"Claims", "[Student, Tutor]" }
            };

            return JWTokenManager.GenerateToken(testDict);
        }
    }
}
