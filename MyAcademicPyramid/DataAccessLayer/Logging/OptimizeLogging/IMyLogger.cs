using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Logging
{
    interface IMyLogger
    {
        void ConfigureLogger();
        void LogError();
        void LogTelemetry(bool optOut);
    }
}
