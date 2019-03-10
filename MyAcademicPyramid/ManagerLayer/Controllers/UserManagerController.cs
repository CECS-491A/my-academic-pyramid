using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using ManagerLayer.UserManagement;
using ServiceLayer.PasswordChecking.HashFunctions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ManagerLayer.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserManagerController : ApiController
    {


        // GET api/<controller>
        [HttpGet]
        public IQueryable<UserDTO> Get()
        {
            UserManager uM = new UserManager();
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
            HashSalt hashSaltPassword = HashFunction.GetHashValue(userDto.RawPassword);
            User user = new User
            {
                UserName = userDto.UserName,
                Firstname = userDto.Firstname,
                LastName = userDto.LastName,
                PasswordHash = hashSaltPassword.Hash,
                PasswordSalt = hashSaltPassword.Salt,
                Role = userDto.Role,
                CreatedDate = DateTime.Now,
                BirthDate = userDto.BirthDate,
                Location = userDto.Location,
                Email = userDto.Email,
                PasswordQuestion1 = userDto.PasswordQuestion1,
                PasswordQuestion2 = userDto.PasswordQuestion2,
                PasswordQuestion3 = userDto.PasswordQuestion3,
                PasswordAnswer1 = userDto.PasswordAnswer1,
                PasswordAnswer2 = userDto.PasswordAnswer2,
                PasswordAnswer3 = userDto.PasswordAnswer3,
            };

            UserManager uM = new UserManager(_dbContext);
             uM.CreateUserAccount(user);
            _dbContext.SaveChanges();

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