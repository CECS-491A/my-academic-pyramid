using DataAccessLayer;
using DataAccessLayer.DTOs;
using ManagerLayer.UserManagement;
using ServiceLayer.PasswordChecking.HashFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ManagerLayer.Controllers
{
    public class UserManagerController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public IQueryable<UserDTO> Get()
        {
            DatabaseContext DbContext = new DatabaseContext();
            UserManager uM = new UserManager(DbContext);
            List<User> userList = uM.GetAllUser();

            List<UserDTO> list = new List<UserDTO>();
            foreach(var user in userList)
            {
                list.Add(new UserDTO
                {
                    UserName = user.UserName,
                    Firstname = user.Firstname,
                    LastName = user.LastName,
                    Email = user.Email
                });
            }

            return list.AsQueryable();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Post([FromBody] UserDTO userDto)
        {
            SHA256HashFunction HashFunction = new SHA256HashFunction();
            String _passwordHash = HashFunction.GetHashValue(userDto.Password);
            User user = new User
            {
                UserName = userDto.UserName,
                Firstname = userDto.Firstname,
                LastName = userDto.LastName,

            };

            DatabaseContext DbContext = new DatabaseContext();
            UserManager uM = new UserManager(DbContext);
             uM.CreateUserAction(user, _passwordHash);
            DbContext.SaveChanges();

            return Ok(user);



        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}