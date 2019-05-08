using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using NLog.Fluent;
using DataAccessLayer.Logging;
using NLog;
//using NLog.Web;


namespace DemoProject
{
    class ProgramLogging
    {
        // Creating a new Logger has an overhead, as it has to acquire locks and allocate objects.
        // Therefore it is recommended to create the logger like this:
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {

            logger.Error("Only errors");

            logger.Info("Login");

            //logger.Fatal("100 fails ");
        }
    }
}
