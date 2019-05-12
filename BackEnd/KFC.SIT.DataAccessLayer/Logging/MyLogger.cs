using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;

// TODO - delete 
// Log using NLOG
namespace DataAccessLayer.Logging
{
    public class MyLogger
    {
        private Logger _logger;
        private static int _failCount; 

        public MyLogger(string name)
        {
            _logger = LogManager.GetLogger(name);
        }

        public void LogError(DateTime errorDate, string errorMessage, string userName, string request, [CallerMemberName] string targetSite = "")
        {
            LogEventInfo logError = new LogEventInfo(
                LogLevel.Error, _logger.Name, errorMessage);
            logError.Properties["Date"] = DateTime.Now;
            logError.Properties["Message"] = errorMessage;
            logError.Properties["User Name"] = userName;
            logError.Properties["Target Site"] = targetSite;
            logError.Properties["Request"] = request;
            _logger.Log(typeof(MyLogger), logError);
        }

        public void LogLogin(DateTime loginDate, string userName, string userIPAddress, string userLocation)
        {
            LogEventInfo logLogin = new LogEventInfo(
                LogLevel.Info, _logger.Name, "Login");
            logLogin.Properties["Login Date"] = DateTime.Now;
            logLogin.Properties["User Name"] = userName; 
            logLogin.Properties["User IP Address"] = userIPAddress;
            logLogin.Properties["Location"] = userLocation;
            _logger.Log(typeof(MyLogger), logLogin);
        }

        public void LogLogout(DateTime logoutDate, string userName, DateTime sessionTime)
        {
            LogEventInfo logLogout = new LogEventInfo(
                LogLevel.Info, _logger.Name, "Logout");
            logLogout.Properties["Login Date"] = DateTime.Now;
            logLogout.Properties["User Name"] = userName;
            logLogout.Properties["Session Time"] = sessionTime;
            _logger.Log(typeof(MyLogger), logLogout);
        }

        public void LogPageVisit(DateTime pageVisitDate, string userName, string page)
        {
            LogEventInfo logPageVisit = new LogEventInfo(
                LogLevel.Info, _logger.Name, "Page Visit");
            logPageVisit.Properties["Page"] = page;
            logPageVisit.Properties["Visit Date"] = DateTime.Now;
            logPageVisit.Properties["User Name"] = userName;
            _logger.Log(typeof(MyLogger), logPageVisit);
        }

        public void LogFunctionality(DateTime functionalityDate, string userName, string page, [CallerMemberName] string functionality = "")
        {
            LogEventInfo logFunctionality = new LogEventInfo(
                LogLevel.Info, _logger.Name, "Functionality");
            logFunctionality.Properties["Functionality"] = DateTime.Now;
            logFunctionality.Properties["Date"] = DateTime.Now;
            logFunctionality.Properties["Page"] = userName;
            logFunctionality.Properties["Functionality"] = functionality;
            _logger.Log(typeof(MyLogger), logFunctionality);
        }

        public static void IncreaseFailCount()
        {
            Interlocked.Increment(ref _failCount);
            if(_failCount == 100)
            {
                // email system admin 

            }
        }
    }
}
