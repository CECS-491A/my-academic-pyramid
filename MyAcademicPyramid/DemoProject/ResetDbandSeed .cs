
using DataAccessLayer;


namespace DemoProject
{
    public class ResetDbandSeed
    {
        public void Reseed()
        {
            using (var context = new DatabaseContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM  [UserClaims]  WHERE User_Id  NOT IN (SELECT TOP 1 User_Id  FROM [UserClaim])") ;
                context.Database.ExecuteSqlCommand("DELETE FROM  [Claims]  WHERE Id  NOT IN (SELECT TOP 1 Id FROM [Claims]) ");
                context.Database.ExecuteSqlCommand("DELETE FROM  [Users] WHERE Id  NOT IN (SELECT TOP 1 Id FROM  [Users]) ");
            }

        }




    
    }
    
}
