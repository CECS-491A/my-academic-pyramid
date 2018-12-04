using System;
using System.Collections.Generic;
using DataAccessLayer.Repository;

namespace DataAccessLayer
{
    public class User : IEntity
    {

        // Constructor which accept username and id 
        public User(string userName, int id) 
        {
            Id = id;
            UserName = userName;
            Claims = new List<string>();
        }

        public int Id { get; set; }

        public string UserName { get; set; }


        // List of string to store user Claims
        public List<String> Claims { get; set; }
        
    }




}