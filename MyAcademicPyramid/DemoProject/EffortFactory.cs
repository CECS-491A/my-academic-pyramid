using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DemoProject
{
   public class EffortFactory : IDbConnectionFactory
    {
        // Handle one instance of DbConnection when Entity FrameWork ask for this 
        private static DbConnection connection;
        private static object _lock = new object(); 

        // Method used to dispose the previous connection
        public static void ResetDb()
        {
            lock(_lock)
            {
                connection = null;
            }
        }


        public DbConnection CreateConnection(string nameOrConnectionString)
        {
           
            lock (_lock)
            {
                if (connection == null)
                {
                    connection = Effort.DbConnectionFactory.CreateTransient();
                }

                return connection;
            }
        }
    }
}
