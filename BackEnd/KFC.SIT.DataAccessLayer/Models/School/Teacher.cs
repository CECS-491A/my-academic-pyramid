using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class Teacher
    {
        public Teacher(int id, string firstName, string middleName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Schools = new List<SchoolTeacher>();
        }

        public Teacher(int id, string firstName, string lastName)
        {
            Id = id;
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

        public ICollection<SchoolTeacher> Schools { set; get; }
    }
}
