using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.School
{
    public class Teacher
    {
        public Teacher(string firstName, string middleName, string lastName, int departmentId)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            DepartmentId = departmentId;
            Schools = new List<School>();
        }

        public Teacher(string firstName, string lastName, int departmentId)
        {
            FirstName = firstName;
            LastName = lastName;
            DepartmentId = departmentId;
            Schools = new List<School>();
        }

        [Required, Key]
        public int Id { set; get; }
        [Required]
        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        [Required]
        public string LastName { set; get; }
        [ForeignKey("Department")]
        public int DepartmentId { set; get; }
        public Department Department { set; get; }

        public virtual ICollection<School> Schools { set; get; }
    }
}
