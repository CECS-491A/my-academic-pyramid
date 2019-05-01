using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class Department
    {
        public Department()
        {
            Schools = new List<School>();
            Students = new List<Student>();
        }

        public Department(string name)
        {
            Name = name;
            Schools = new List<School>();
            Students = new List<Student>();
        }

        [Required, Key]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }

        public virtual ICollection<School> Schools { set; get; }
        public virtual ICollection<Student> Students { set; get; }
    }
}
