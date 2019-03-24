using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SecurityLayer;
using DataAccessLayer;
using DataAccessLayer.DTOs;
using ManagerLayer.UserManagement;

namespace KFC.SIT.WebAPI.Controllers
{
    public class LogoutController : ApiController
    {
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            // TODO invalidate the token.
            DatabaseContext db = new DatabaseContext();
            JWTokenManager jm = new JWTokenManager(db);
            jm.InvalidateToken(value);
        }
    }
}