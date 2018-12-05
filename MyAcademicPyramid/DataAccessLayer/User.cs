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

        public User(string userName)
        {
            UserName = userName;
            Claims = new List<string>();
        }
        
        public int Id { get; set; }

        public string UserName { get; set; }

        // List of string to store user Claims
        public List<String> Claims { get; set; }


        public override bool Equals(object obj)
        {
            if (!(obj is User item))
            {
                return false;
            }

            return this.UserName.Equals(item.UserName);
        }

        public override int GetHashCode()
        {
            return this.UserName.GetHashCode();
        }

    }




}