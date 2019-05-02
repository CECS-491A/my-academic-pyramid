using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.School
{
    public class Teacher: IEntity
    {
        public Teacher(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Schools = new List<SchoolTeacher>();
        }

        public Teacher(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Schools = new List<SchoolTeacher>();
        }

        [Required, Key]
        public int Id { set; get; }
        [Required]
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        [Required]
        public string LastName { set; get; }

        public virtual ICollection<SchoolTeacher> Schools { set; get; }
    }
}
