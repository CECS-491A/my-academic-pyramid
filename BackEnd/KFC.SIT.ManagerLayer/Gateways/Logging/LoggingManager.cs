using DataAccessLayer.Models;
using MongoDB.Driver;
using ServiceLayer.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebAPI.Gateways.UserManagement;

namespace ManagerLayer.Gateways.Logging
{
    public class LoggingManager
    {
        private LoggingServices _loggingService = new LoggingServices();
        private static int _errorFailCount;
        private static int _telemetryFailCount;
        private UserManager _userManager = new UserManager();

        public void LogError(string userName, string request, string errorMessage, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string targetSite = "", [CallerLineNumber] int lineOfCode = 0)
        {
            try
            {
                ErrorLog errorLog= new ErrorLog()
                {
                    Date = DateTime.Now.ToUniversalTime(),
                    Message = errorMessage,
                    UserName = userName,
                    TargetSite = callerFilePath + targetSite,
                    LineOfCode = lineOfCode.ToString(),
                    Request = request
                };
                //Interlocked.Increment(ref _errorFailCount);
                _loggingService.LogError(errorLog);
            }
            catch (Exception)
            {
                // increase locked errorFailCount
                Interlocked.Increment(ref _errorFailCount);
                if (_errorFailCount == 100)
                {
                    // email system admin 
                }
            }
        }

        // Todo - add location 
        // Todo - check that we can log telemetry for user
        public void LogLogin(string userName, string ipAdress)
        {
            //if(!CanLogTelemetryForUser(userName))
            //{
            //    return;
            //}
            try
            {
                TelemetryLog loginLog = new TelemetryLog()
                {
                    Date = DateTime.Now.ToUniversalTime(),
                    UserName = userName,
                    Action = "Login", 
                    UserIPAddress = ipAdress
                    //UserLocation = location
                };

                _loggingService.LogTelemetry(loginLog);
            }
            catch (Exception)
            {
                // increase locked errorFailCount
                Interlocked.Increment(ref _telemetryFailCount);
                if (_errorFailCount == 100)
                {
                    // email system admin 
                }
            }
        }

        public void LogLogout(string userName, DateTime sessionDuration)
        {
            //if (!CanLogTelemetryForUser(userName))
            //{
            //    return;
            //}
            try
            {
                TelemetryLog logoutLog = new TelemetryLog()
                {
                    Date = DateTime.Now.ToUniversalTime(),
                    UserName = userName,
                    Action = "Logout",
                    SessionDuration = sessionDuration
                };

                _loggingService.LogTelemetry(logoutLog);
            }
            catch (Exception)
            {
                // increase locked errorFailCount
                Interlocked.Increment(ref _telemetryFailCount);
                if (_errorFailCount == 100)
                {
                    // email system admin 
                }
            }
        }

        public void LogPageVisit(string userName, string page)
        {
            //if (!CanLogTelemetryForUser(userName))
            //{
            //    return;
            //}
            try
            {
                TelemetryLog pageVisitLog = new TelemetryLog()
                {
                    Date = DateTime.Now.ToUniversalTime(),
                    UserName = userName,
                    Action = page
                };

                _loggingService.LogTelemetry(pageVisitLog);
            }
            catch (Exception)
            {
                // increase locked errorFailCount
                Interlocked.Increment(ref _telemetryFailCount);
                if (_errorFailCount == 100)
                {
                    // email system admin 
                }
            }
        }

        public void LogFunctionality(string userName, string functionality)
        {
            //if (!CanLogTelemetryForUser(userName))
            //{
            //    return;
            //}
            try
            {
                TelemetryLog functionalityLog = new TelemetryLog()
                {
                    Date = DateTime.Now.ToUniversalTime(),
                    UserName = userName,
                    Action = functionality
                };

                _loggingService.LogTelemetry(functionalityLog);
            }
            catch (Exception)
            {
                // increase locked errorFailCount
                Interlocked.Increment(ref _telemetryFailCount);
                if (_errorFailCount == 100)
                {
                    // email system admin 
                }
            }
        }

        public async void LogRequest(string userName, string request)
        {
            try
            {
                RequestLog requestLog = new RequestLog()
                {
                    Date = DateTime.Now.ToUniversalTime(),
                    UserName = userName,
                    Request = request
                };

                _loggingService.LogRequest(requestLog);
                await _loggingService.MonitorForDOSAttack();
            }
            catch (Exception)
            {
                // increase locked errorFailCount
                Interlocked.Increment(ref _telemetryFailCount);
                if (_errorFailCount == 100)
                {
                    // email system admin 
                }
            }
        }

        // return true or false 
        public bool CanLogTelemetryForUser(string username)
        {
            var account = _userManager.FindByUserName(username);
            return account.LogTelemetry;
        }

        // TODO - delete
        public int getErrorFailCount()
        {
            return _errorFailCount;
        }
    }
}