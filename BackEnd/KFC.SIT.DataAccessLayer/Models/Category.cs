using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Category : IEntity
    {
        public Category()
        {
            
            this.Users = new HashSet<Account>();
        }
        public Category(string value)
        {
            Value = value;
            this.Users = new HashSet<Account>();
        }

        //Category Id

        public int Id { get; set; }

        //Category Value
        public string Value { get; set; }

        //Collection of Users that used for one-many relationship with user class 
       
        public ICollection<Account> Users { get; set; }
    }
}
