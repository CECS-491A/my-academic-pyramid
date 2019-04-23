using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Exp { get; set; }
        public string Category { get; set; }
        public String DateOfBirth { get; set; }
        //public String Location { get; set; }
        public string Email { get; set; }
        public String CreatedAt { get; set; }
    }
}
