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
using System.Web.Http.Cors;

namespace ManagerLayer.Controllers
{
    [EnableCors(origins: "https://myacademicpyramid.com", headers: "*", methods: "*")]
    public class UserManagerController : ApiController
    {
        private readonly DatabaseContext _dbContext; 
        public UserManagerController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/<controller>
        [HttpGet]
        public IQueryable<UserDTO> Get()
        {
            UserManager uM = new UserManager(_dbContext);
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
            String passwordHash = HashFunction.GetHashValue(userDto.RawPassword);
            User user = new User
            {
                UserName = userDto.UserName,
                Firstname = userDto.Firstname,
                LastName = userDto.LastName,

            };

            DatabaseContext DbContext = new DatabaseContext();
            UserManager uM = new UserManager(DbContext);
             uM.CreateUserAccount(user, passwordHash);
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