using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DataAccessLayer.Logging;
using DataAccessLayer.Models;

namespace ServiceLayer.Logging
{
    public class LoggingServices
    {
        //private Logger _logger;
        private LogRepository<ErrorLog> _errorLogRepository = new LogRepository<ErrorLog>("ErrorLogs");
        private LogRepository<TelemetryLog> _telemetryLogRepository = new LogRepository<TelemetryLog>("TelemetryLogs");
        private LogRepository<RequestLog> _requestLogRepository = new LogRepository<RequestLog>("RequestLogs");


        public void LogError(ErrorLog errorLog)
        {
            _errorLogRepository.CreateLog(errorLog);
        }

        public void LogTelemetry(TelemetryLog telemetryLog)
        {
            _telemetryLogRepository.CreateLog(telemetryLog);
        }

        public ErrorLog GetErrorLog(string errorLogId)
        {
            return _errorLogRepository.GetLog(errorLogId);
        }

        public TelemetryLog GetTelemetryLog(string telemetryLogId)
        {
            return _telemetryLogRepository.GetLog(telemetryLogId);
        }

        public List<ErrorLog> GetAllErrorLogs()
        {
            return _errorLogRepository.GetAllLogs();
        }

        public List<TelemetryLog> GetAllTelemetryLogs()
        {
            return _telemetryLogRepository.GetAllLogs();
        }

        public void DeleteErrorLog(string errorLogId)
        {
            _errorLogRepository.DeleteLog(errorLogId);
        }

        public void DeleteTelemetryLog(string telemetryLogId)
        {
            _telemetryLogRepository.DeleteLog(telemetryLogId);
        }

        public void DeleteAllErrorLogs()
        {
            _errorLogRepository.DeleteAllLogs();
        }

        public void DeleteAllTelemetryLogs()
        {
            _telemetryLogRepository.DeleteAllLogs();
        }

        public void DeleteUserTelemetryLogs(string userName)
        {
            _telemetryLogRepository.DeleteAllLogsForUser(userName);
        }





        public void LogRequest(RequestLog requestLog)
        {
            _requestLogRepository.CreateLog(requestLog);
        }

        //public List<RequestLog> GetAllRequestLogs()
        //{
        //    return _requestLogRepository.GetAllLogs();
        //}

        public void DeleteAllRequestLogs()
        {
            _requestLogRepository.DeleteAllLogs();
        }

        public async Task MonitorForDOSAttack()
        {
            //DateTime startTime = DateTime.Now;

            long firstRequestCount = await _requestLogRepository.CountLogs();

            int delayTime = 1000;
            // wait 1 sec
            await Task.Delay(delayTime);

            long secondRequestCount = await _requestLogRepository.CountLogs();

            long requestsPerSecond = secondRequestCount - firstRequestCount;

            if (requestsPerSecond > 10000 )
            {
                // email sys Admin 
            }

            Console.WriteLine("FRequestCount: " + firstRequestCount + "\nElapsedTime: " + delayTime +
                "\n SRequestCount: " + secondRequestCount);
        }
    }
}