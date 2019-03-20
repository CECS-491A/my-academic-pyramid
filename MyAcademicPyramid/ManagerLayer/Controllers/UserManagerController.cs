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
            UserManager umManager = new UserManager();
            List<User> userList = umManager.GetAllUser();

            List<UserDTO> list = new List<UserDTO>();
            foreach(var user in userList)
            {
                list.Add(new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });
            }

            return list.AsQueryable();
        }

        // GET api/<controller>/5
        public string Get(Guid id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Post([FromBody] UserDTO userDto)
        {
            UserManager umManager = new UserManager();
            return Ok(umManager.CreateUserAccount(userDto));
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult Put([FromBody] UserDTO userDto)
        {
            UserManager umManager = new UserManager();
            User foundUser = umManager.FindUserById(userDto.Id);
            foundUser.UserName = userDto.UserName;
            foundUser.FirstName = userDto.FirstName;
            foundUser.LastName = userDto.LastName;
            return Ok(umManager.UpdateUserAccount(foundUser));
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
            UserManager umManager = new UserManager();
            umManager.DeleteUserAccount(umManager.FindUserById(id));
        }
    }
}