using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class Department
    {
        public Department(int id, string name)
        {
            Id = id;
            Name = name;

            Schools = new List<SchoolDepartment>();
        }

        [Required, Key]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }

        public ICollection<SchoolDepartment> Schools { set; get; }
    }
}
