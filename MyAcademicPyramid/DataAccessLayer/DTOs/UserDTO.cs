﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class UserDTO
    {

        public string UserName { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public DateTime BirthDate { get; set; }
        public String Location { get; set; }
        public string Email { get; set; }
        public String CreatedDate { get; set; }
        public String RawPassword { get; set; }
        public String PasswordQuestion1 { get; set; }
        public String PasswordQuestion2 { get; set; }
        public String PasswordQuestion3 { get; set; }
        public String PasswordAnswer1 { get; set; }
        public String PasswordAnswer2 { get; set; }
        public String PasswordAnswer3 { get; set; }

    }
}