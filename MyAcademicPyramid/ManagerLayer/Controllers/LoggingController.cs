using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer.Models;
using DataAccessLayer.Logging;



namespace ManagerLayer.Controllers
{
    //readonly MongoDatabase mongoDatabase;

    public class LoggingController : ApiController
    {
        //mongoDatabase = RetrieveMongohqDB();
        private ErrorLogCollection _errors = new DataAccessLayer.Logging.ErrorLogCollection();
        private ErrorLogCollection _telemetries = new DataAccessLayer.Logging.ErrorLogCollection();


        [HttpGet]
        [Route("api/logging/geterrors")]
        public List<ErrorLog> ListErrors()
        {
            return _errors.GetAll();
        }

        [HttpGet]
        [Route("api/logging/gettelemetries")]
        public List<ErrorLog> ListTelemetries()
        {
            return _telemetries.GetAll();
        }

        [HttpPost]
        [Route("api/logging/deleteerrors")]
        public IHttpActionResult DeleteAllErrors()
        {
            _errors.DeleteAll();

            return Ok();
        }

        [HttpPost]
        [Route("api/logging/deletetelemetries")]
        public IHttpActionResult DeleteAllTelemetries()
        {
            _errors.DeleteAll();

            return Ok();
        }






    }
}
