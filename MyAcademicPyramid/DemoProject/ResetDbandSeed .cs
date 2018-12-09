using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace DemoProject
{
    public class ResetDbandSeed
    {
        public void Reseed()
        {
            using (var context = new myAPEntities1())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM  [UserClaim]  WHERE User_Id  NOT IN (SELECT TOP 2 User_Id  FROM [UserClaim])") ;
                context.Database.ExecuteSqlCommand("DELETE FROM  [Claims]  WHERE Id  NOT IN (SELECT TOP 2 Id FROM [Claims]) ");
                context.Database.ExecuteSqlCommand("DELETE FROM  [Users] WHERE Id  NOT IN (SELECT TOP 1 Id FROM  [Users]) ");
            }

        }




    
    }
    
}
