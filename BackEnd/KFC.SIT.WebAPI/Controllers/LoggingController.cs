using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer.Models;
using DataAccessLayer.Logging;
using System.Web.Http.Cors;


namespace KFC.SIT.WebAPI
{
    //readonly MongoDatabase mongoDatabase;
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    public class LoggingController : ApiController
    {
        //mongoDatabase = RetrieveMongohqDB();
        private ErrorLogCollection _errors = new DataAccessLayer.Logging.ErrorLogCollection();
        private TelemetryLogCollection _telemetries = new DataAccessLayer.Logging.TelemetryLogCollection();


        [HttpGet]
        [Route("api/logging/geterrors")]
        public List<ErrorLog> ListErrors()
        {
            return _errors.GetAll();
        }

        [HttpGet]
        [Route("api/logging/gettelemetries")]
        public List<TelemetryLog> ListTelemetries()
        {
            return _telemetries.GetAll();
        }

        [HttpDelete]
        [Route("api/logging/deleteerror")]
        public void DeleteError(string id)
        {
            _errors.Delete(id);

            //return Ok();
        }

        [HttpDelete]
        [Route("api/logging/deletetelemetry")]
        public IHttpActionResult DeleteTelemetry(string id)
        {
            _telemetries.Delete(id);

            return Ok();
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
            _telemetries.DeleteAll();

            return Ok();
        }






    }
}
