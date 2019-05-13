using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer.Models;
using DataAccessLayer.Logging;
using System.Web.Http.Cors;
using ServiceLayer.Logging;
using ManagerLayer.Gateways.Logging;
using System.Web;

namespace KFC.SIT.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoggingController : ApiController
    {
        private LoggingServices _loggingServices = new LoggingServices();
        private static LoggingManager _loggingManager = new LoggingManager();

        [HttpGet]
        [ActionName("GetErrorLogs")]
        public IHttpActionResult GetErrorLogs()
        {
            var errorLogs =  _loggingServices.GetAllErrorLogs();
            return Content(HttpStatusCode.OK, errorLogs);
        }

        [HttpGet]
        [ActionName("GetTelemetryLogs")]
        public List<TelemetryLog> GetTelemetryLogs()
        {
            return _loggingServices.GetAllTelemetryLogs();
        }

        [HttpDelete]
        [ActionName("DeleteErrorLog")]
        public void DeleteErrorLog(string errorLogId)
        {
            _loggingServices.DeleteErrorLog(errorLogId);
            //return Ok();
        }

        [HttpDelete]
        [ActionName("DeleteTelemetryLog")]
        public IHttpActionResult DeleteTelemetryLog(string telemetryLogId)
        {
            _loggingServices.DeleteTelemetryLog(telemetryLogId);

            return Ok();
        }

        [HttpPost]
        [ActionName("LogPageVisit")]
        public IHttpActionResult LogPageVisit(string userName, string page)
        {
            _loggingManager.LogLogin(userName, page);
            string ip = HttpContext.Current.Request.UserHostAddress;

            string request = HttpContext.Current.Request.HttpMethod;
            _loggingManager.LogRequest(userName, request);

            return Ok();
        }

        // IP Address
        // string ipAddress = HttpContext.Current.Request.UserHostAddress
        //_logger.LogLogin(userName, ipAddress);

        // Functionality 
        // string functionality = this.ControllerContext.RouteData.Values["action"].ToString();






        // delete later. Don't need
        private static LoggingManager _logger = new LoggingManager();

        [HttpGet]
        [ActionName("TestLogger")]
        public void TestLogger()
        {
            try
            {
                int a = 0;
                int b = 0;
                int c = a / b;
            }
            catch (Exception ex)
            {
                _logger.LogError("Arturo", "request", ex.Message);
            }
        }

        [HttpGet]
        [ActionName("TestFails")]
        public IHttpActionResult TestFails()
        {
            try
            {
                int a = 0;
                int b = 0;
                int c = a / b;
            }
            catch (Exception ex)
            {
                _logger.LogError("Arturo", "request", ex.Message);
            }
            return Content(HttpStatusCode.OK, _logger.getErrorFailCount());
        }

        [HttpGet]
        [Route("TestIP")]
        public IHttpActionResult TestIP()
        {
            //_telemetries.DeleteAll();
            _logger.LogPageVisit("Arturo", HttpContext.Current.Request.UserHostAddress);
            return Ok();
        }


        // TODO - delete
        [HttpPost]
        [ActionName("DeleteAllErrorLogs")]
        public IHttpActionResult DeleteAllErrorLogs()
        {
            _loggingServices.DeleteAllErrorLogs();

            return Ok();
        }

        // TODO - delete 
        [HttpPost]
        [ActionName("DeleteAllTelemetryLogs")]
        public IHttpActionResult DeleteAllTelemetryLogs()
        {
            _loggingServices.DeleteAllTelemetryLogs();

            return Ok();
        }

        // Todo - Delete. call service where account is deleted
        [HttpPost]
        [ActionName("DeleteAllTelemetryLogsForUser")]
        public IHttpActionResult DeleteAllTelemetryLogsForUser(string userName)
        {
            _loggingServices.DeleteUserTelemetryLogs(userName);

            return Ok();
        }
    }
}
