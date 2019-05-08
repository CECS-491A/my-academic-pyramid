using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace DataAccessLayer.Logging
{
    class MyLogger
    {
        private Logger _logger;

        public MyLogger(string name)
        {
            _logger = LogManager.GetLogger(name);
        }

        public void LogError(DateTime errorDate, string errorMessage, string targetSite, string userName, string request)
        {

        }

        public void LogTelemetry(DateTime loginDate, DateTime logoutDate, DateTime pageVisit, DateTime functionality, string userIPAddress, string userLocation)
        {

        }
    }
}
