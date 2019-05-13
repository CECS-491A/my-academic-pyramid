using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using NLog.Fluent;
using DataAccessLayer.Logging;
using NLog;
using System.Runtime.CompilerServices;
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

            //logger.Error("Only errors");

            //logger.Info("Login");

            LogThis("Message");

            MyLogger myLogger = new MyLogger("ProgramLogging");

            myLogger.LogError(DateTime.Now, "this error", "Arturo", "login");


            //logger.Fatal("100 fails ");

        }

        public static void LogThis(string message, [CallerFilePath] string callerFilePath = "", 
            [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            System.Diagnostics.Trace.WriteLine("message: " + message);
            System.Diagnostics.Trace.WriteLine("source file path: " + callerFilePath);
            System.Diagnostics.Trace.WriteLine("member name: " + memberName);
            System.Diagnostics.Trace.WriteLine("source line number: " + sourceLineNumber);

            ApplyFormat(callerFilePath);
            System.Diagnostics.Trace.WriteLine("\n\n\n: " + callerFilePath);

        }

        public static void ApplyFormat(string path)
        {
            
        }

    }
}