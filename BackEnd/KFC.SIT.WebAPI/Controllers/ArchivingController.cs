using System;
using System.Collections.Generic;
using System.Web.Http;
using DataAccessLayer.Models;
using DataAccessLayer.Logging;
using System.Globalization;
using System.IO;
using System.IO.Compression;

namespace KFC.SIT.WebAPI.Controllers
{
    public class ArchivingController : ApiController
    {

        private static ErrorLogCollection _errors = new DataAccessLayer.Logging.ErrorLogCollection();
        private static TelemetryLogCollection _telemetries = new DataAccessLayer.Logging.TelemetryLogCollection();
        private const int maxLogLife = 30;
        private static int errorcount = 0;
        private static string archivePath = @"My-Academic-Pyramid\Archives";
        // GET: Archiving
        public static List<string> GetErrorLogs()
        {

            List<ErrorLog> allErrorLogs = _errors.GetAll();
            List<string> errorLogList = new List<string>();
            foreach (ErrorLog errorpath in allErrorLogs)
            {
                errorLogList.Add(errorpath.ID);
            }
            return errorLogList;
        }

        public static List<string> GetTelemetryLogs()
        {
            List<TelemetryLog> allTelemetryLogs = _telemetries.GetAll();
            List<string> telemetryLogList = new List<string>();
            foreach (TelemetryLog telemetryPath in allTelemetryLogs)
            {
                telemetryLogList.Add(telemetryPath.ID);
            }
            return telemetryLogList;
        }


        public static List<String> GetOldErrorLogs()
        {
            List<string> logList = GetErrorLogs();
            List<string> oldErrorLogList = new List<string>();
            foreach (string logID in logList)
            {
                if (checkOldErrorLog(logID) == true)
                {
                    oldErrorLogList.Add(logID);
                }
            }
            return oldErrorLogList;
        }
        public static List<String> GetOldTelemetryLogs()
        {
            List<string> logList = GetTelemetryLogs();
            List<string> oldTelemetryLogList = new List<string>();
            foreach (string logID in logList)
            {
                if (checkOldTelemetryLog(logID) == true)
                {
                    oldTelemetryLogList.Add(logID);
                }
            }
            return oldTelemetryLogList;
        }
        public static bool checkOldErrorLog(string fileId)
        {
            ErrorLog errorfile = _errors.Get(fileId);
            bool OldLog = false;
            if ((DateTime.Now - errorfile.Date).TotalDays > maxLogLife)
            {
                OldLog = true;
            }
            return OldLog;

        }
        public static bool checkOldTelemetryLog(string fileId)
        {
            TelemetryLog errorfile = _telemetries.Get(fileId);
            bool OldLog = false;
            if ((DateTime.Now - errorfile.Date).TotalDays > maxLogLife)
            {
                OldLog = true;
            }
            return OldLog;

        }

        public static bool ArchiveOldLogFiles()
        {
            bool successfulArchive = false;
            bool successfulDelete = false;
            List<string> ErrorLogs = GetOldErrorLogs();
            List<string> TelemetryLogs = GetOldTelemetryLogs();
            string telemetryArchiveName = DateTime.Now.ToString("dd-MM-yyyy") + "_TelemetryArchive.zip";
            string errorArchiveName = DateTime.Now.ToString("dd-MM-yyyy") + "_ErrorArchive.zip";


            try
            {
                using (FileStream filestreamer = new FileStream((archivePath + telemetryArchiveName), FileMode.OpenOrCreate))
                {
                    using (ZipArchive archiver = new ZipArchive(filestreamer, ZipArchiveMode.Update))
                    {
                        foreach (string logId in TelemetryLogs)
                        {
                            TelemetryLog telemetryFile = _telemetries.Get(logId);
                            ZipArchiveEntry logEntry = archiver.CreateEntry(telemetryFile.ToString());

                            successfulDelete = RemoveTelemetryLog(logId);
                        }
                        if (successfulDelete == true)
                        {
                            archiver.Dispose();
                            successfulArchive = true;
                        }

                    }
                }

            }
            catch (FileNotFoundException e)
            {
                successfulArchive = false;
                errorcount++;
            }
            try
            {
                using (FileStream filestreamer = new FileStream((archivePath + errorArchiveName), FileMode.OpenOrCreate))
                {
                    using (ZipArchive archiver = new ZipArchive(filestreamer, ZipArchiveMode.Update))
                    {
                        foreach (string logId in ErrorLogs)
                        {
                            ErrorLog errorFile = _errors.Get(logId);
                            ZipArchiveEntry logEntry = archiver.CreateEntry(errorFile.ToString());

                            successfulDelete = RemoveErrorLog(logId);
                        }
                        if (successfulDelete == true)
                        {
                            archiver.Dispose();
                            successfulArchive = true;
                        }

                    }
                }

            }
            catch (FileNotFoundException e)
            {
                successfulArchive = false;
                errorcount++;
            }

            return successfulArchive;
        }



        private static bool RemoveTelemetryLog(string logId)
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
        private static bool RemoveErrorLog(string logId)
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
    
        public static void contactAdmin()
        {
          if (errorcount >= 3)
            {
                //System admin notified.
                errorcount = 0;
            }
        }
    }
}