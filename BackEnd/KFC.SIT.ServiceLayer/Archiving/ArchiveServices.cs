using System;
using System.Collections.Generic;
using DataAccessLayer.Models;
using DataAccessLayer.Logging;
using System.IO;

namespace ServiceLayer.Archiving
{
    public class ArchiveServices
    {
        private static ErrorLogCollection _errors = new DataAccessLayer.Logging.ErrorLogCollection();
        private static TelemetryLogCollection _telemetries = new DataAccessLayer.Logging.TelemetryLogCollection();
        private static int errorcount = 0;

        public TelemetryLog GetTelemetryLog(string Id)
        {
            return _telemetries.Get(Id);
        }

        public ErrorLog GetErrorLog(string Id)
        {
            return _errors.Get(Id);
        }

        // GET: Archiving
        public List<string> GetErrorLogs()
        {

            List<ErrorLog> allErrorLogs = _errors.GetAll();
            List<string> errorLogList = new List<string>();
            foreach (ErrorLog errorpath in allErrorLogs)
            {
                errorLogList.Add(errorpath.ID);
            }
            return errorLogList;
        }

        public List<string> GetTelemetryLogs()
        {
            List<TelemetryLog> allTelemetryLogs = _telemetries.GetAll();
            List<string> telemetryLogList = new List<string>();
            foreach (TelemetryLog telemetryPath in allTelemetryLogs)
            {
                telemetryLogList.Add(telemetryPath.ID);
            }
            return telemetryLogList;
        }


        public List<String> GetOldErrorLogs(int maxLogLife)
        {
            List<string> logList = GetErrorLogs();
            List<string> oldErrorLogList = new List<string>();
            foreach (string logID in logList)
            {
                if (checkOldErrorLog(logID, maxLogLife) == true)
                {
                    oldErrorLogList.Add(logID);
                }
            }
            return oldErrorLogList;
        }
        public List<String> GetOldTelemetryLogs(int maxLogLife)
        {
            List<string> logList = GetTelemetryLogs();
            List<string> oldTelemetryLogList = new List<string>();
            foreach (string logID in logList)
            {
                if (checkOldTelemetryLog(logID, maxLogLife) == true)
                {
                    oldTelemetryLogList.Add(logID);
                }
            }
            return oldTelemetryLogList;
        }
        public bool checkOldErrorLog(string fileId, int maxLogLife)
        {
            ErrorLog errorfile = _errors.Get(fileId);
            bool OldLog = false;
            if ((DateTime.Now - errorfile.Date).TotalDays > maxLogLife)
            {
                OldLog = true;
            }
            return OldLog;

        }
        public bool checkOldTelemetryLog(string fileId, int maxLogLife)
        {
            TelemetryLog errorfile = _telemetries.Get(fileId);
            bool OldLog = false;
            if ((DateTime.Now - errorfile.Date).TotalDays > maxLogLife)
            {
                OldLog = true;
            }
            return OldLog;

        }

        public bool RemoveTelemetryLog(string logId)
        {
            bool isSuccessfulRemoval = false;
            try
            {
                _telemetries.Delete(logId);
                isSuccessfulRemoval = true;
            }
            catch (FileNotFoundException e)
            {
                isSuccessfulRemoval = false;
                errorcount++;
            }

            return isSuccessfulRemoval;
        }
        public bool RemoveErrorLog(string logId)
        {
            bool isSuccessfulRemoval = false;
            try
            {
                _telemetries.Delete(logId);
                isSuccessfulRemoval = true;
            }
            catch (FileNotFoundException e)
            {
                isSuccessfulRemoval = false;
                errorcount++;
            }

            return isSuccessfulRemoval;
        }
    }
}