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
        private static DbConnection connection;
        private static object _lock = new object(); 

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
