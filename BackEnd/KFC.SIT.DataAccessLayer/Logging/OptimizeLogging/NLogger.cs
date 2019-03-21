using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Logging
{
    class NLogger : IMyLogger
    {
        private readonly ILogDestination _logDestination;
        private readonly ILogFormat _logFormat;
        private static int _errorFailCount;
        private static int _telemetryFailCount;

        public NLogger()
        {
            ConfigureLogger();
        }

        public void ConfigureLogger()
        {

        }

        public void LogError()
        {
            //if fails -> IncreaseFailCount

        }

        public void LogTelemetry(bool optOut)
        {
            // if optout false -> dont log
            //if fails -> IncreaseFailCount

        }

        public void NotifySystemAdmin()
        {

        }

        public void IncreaseErrorFailCount()
        {
            _errorFailCount++;
            if (_errorFailCount == 100)
            {
                NotifySystemAdmin();
                _errorFailCount = 0;
            }

        }

        public void IncreaseTelemetryFailCount()
        {
            _telemetryFailCount++;
            if (_telemetryFailCount == 100)
            {
                NotifySystemAdmin();
                _telemetryFailCount = 0;
            }
        }

    }
}
