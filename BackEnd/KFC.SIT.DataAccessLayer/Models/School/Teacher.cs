using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class Teacher
    {
        public Teacher(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Schools = new List<School>();
        }

        public Teacher(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Schools = new List<School>();
        }

        [Required, Key]
        public int Id { set; get; }
        [Required]
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        [Required]
        public string LastName { set; get; }

        public virtual ICollection<School> Schools { set; get; }
    }
}
