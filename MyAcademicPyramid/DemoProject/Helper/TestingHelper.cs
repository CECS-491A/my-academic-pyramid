using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Helper
{
    public abstract class TestingHelper
    {
        public static void ClearDatabase()
        {
            DatabaseContext dBcontext = new DatabaseContext();
            dBcontext.Database.Delete();
            dBcontext.Database.Create();
            dBcontext.SaveChanges();

        }
    }
}
