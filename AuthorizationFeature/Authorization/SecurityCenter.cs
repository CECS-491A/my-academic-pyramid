using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authorization.DataAccess;
namespace Authorization
{
    class SecurityCenter
    {
        // Make an authorized user object 
        public AuthorizedUser AuthorizeUser(AuthorizedUser user)
        {
            AuthorizedUser authUserObj = new AuthorizedUser();
            List<Claim> claimList = new List<Claim>();

            authUserObj.UserName = user.UserName;
            authUserObj.UserType = user.UserType;

            // Retrieve claims from the claim table in the database
            authUserObj.UserClaims = GetUserClaim(user);
            return authUserObj;
        }

        // The method will go to the database and get claims of user. The claims is based on type of user (Admin or Student)
        public List<Claim> GetUserClaim(AuthorizedUser authUser)
        {
            List<Claim>list  = new List<Claim>();
            try
            {
                // Create instance of database context using Entity Framework
                using (var db = new DatabaseEntities())
                {
                    list = db.Claims.Where(
                                    u => u.UserType.UserType1 == authUser.UserType).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Trying to get user claims", ex);
            }
            return list; 

        }

        // Method to disable a claim in claim table
        public void DisableClaim(String claim)
        {
            try
            {
                // Create instance of database context using Entity Framework
                using (var db = new DatabaseEntities())
                {
                    var claimObj = db.Claims.SingleOrDefault(c => c.ClaimType.Equals(claim));
                    claimObj.ClaimValue = false;

                    db.Claims.Add(claimObj);
                    db.Claims.Attach(claimObj);
                    db.Entry(claimObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                throw new Exception("Trying to get user claims", ex);
            }
            
        }

        // Method to enable a claim in claim table
        public void EnableClaim(String claim)
        {
            try
            {
                // Create instance of database context using Entity Framework
                using (var db = new DatabaseEntities())
                {
                    var claimObj = db.Claims.SingleOrDefault(c => c.ClaimType.Equals(claim));
                    claimObj.ClaimValue = true;

                    db.Claims.Add(claimObj);
                    db.Claims.Attach(claimObj);
                    db.Entry(claimObj).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Trying to get user claims", ex);
            }

        }


    }
}
