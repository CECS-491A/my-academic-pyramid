using System;
using System.Collections.Generic;
using DataAccessLayer.Repository;

namespace DataAccessLayer
{
    public class User : IEntity
    {
        //List of string to store user Claims
        
        public User(String _userName, int id) 
        {
            Id = id;
            UserName = _userName;
            Claims = new List<string>();
        }

        public String UserName { get; private set; }

        public int Id { get; set; }
        public List<String> Claims { get; set; }



    }




}