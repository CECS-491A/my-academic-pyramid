using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Catergory : IEntity
    {
        public Catergory()
        {
            
            this.Users = new HashSet<User>();
        }
        public Catergory(string value)
        {
            Value = value;
            this.Users = new HashSet<User>();
        }

        //Catergory Id

        public int Id { get; set; }

        //Catergory Value
        public string Value { get; set; }

        //Collection of Users that used for one-many relationship with user class 
       
        public ICollection<User> Users { get; set; }
    }
}
