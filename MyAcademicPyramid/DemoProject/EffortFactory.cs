using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace DemoProject
{
   public class EffortFactory : IDbConnectionFactory
    {
        // Handle one instance of DbConnection when Entity FrameWork ask for this 
        private static DbConnection connection;
        
        //Asynchronous lock to allow single connection to the mock DB
        private static object _lock = new object(); 

        // Method used to dispose the previous connection
        public static void ResetDb()
        {
            lock(_lock)
            {
                connection = null;
            }
        }


         // Method to create connection to database
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
           
            lock (_lock)
            {
                if (connection == null)
                {
                    //Create new instance of database
                    connection = Effort.DbConnectionFactory.CreateTransient();
                }

                return connection;
            }
        }
    }
}
