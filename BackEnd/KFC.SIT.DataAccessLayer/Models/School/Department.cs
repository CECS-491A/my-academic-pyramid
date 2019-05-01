using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class Department
    {
        public Department()
        {
            Schools = new List<SchoolDepartment>();
        }

        public Department(string name)
        {
            Name = name;
            Schools = new List<SchoolDepartment>();
        }

        [Required, Key]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }

        public virtual ICollection<SchoolDepartment> Schools { set; get; }
    }
}
