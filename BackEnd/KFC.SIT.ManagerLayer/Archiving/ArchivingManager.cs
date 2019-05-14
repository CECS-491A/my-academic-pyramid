using System;
using System.Collections.Generic;
using DataAccessLayer.Models;
using System.IO;
using System.IO.Compression;
using ServiceLayer.Archiving;

namespace ManagerLayer.Archiving
{
    public class ArchivingManager
    {
        private static string archivePath = @"My-Academic-Pyramid\Archives";
        ArchiveServices ArchLogs = new ArchiveServices();
        private int errorcount = 0;
        private const int maxLogLife = 30;
        public bool ArchiveOldLogFiles()
        {

            bool successfulArchive = false;
            bool successfulDelete = false;
            List<string> ErrorLogs = ArchLogs.GetOldErrorLogs(maxLogLife);
            List<string> TelemetryLogs = ArchLogs.GetOldTelemetryLogs(maxLogLife);
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
                            TelemetryLog telemetryFile = ArchLogs.GetTelemetryLog(logId);
                            ZipArchiveEntry logEntry = archiver.CreateEntry(telemetryFile.ToString());

                            successfulDelete = ArchLogs.RemoveTelemetryLog(logId);
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
                            ErrorLog errorFile = ArchLogs.GetErrorLog(logId);
                            ZipArchiveEntry logEntry = archiver.CreateEntry(errorFile.ToString());

                            successfulDelete = ArchLogs.RemoveErrorLog(logId);
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


        public void contactAdmin()
        {
            if (errorcount >= 3)
            {
                //System admin notified.
                errorcount = 0;
            }
        }
    }
}