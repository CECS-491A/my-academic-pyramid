using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class School : IEntity
    {
        public School(string name,string contactEmail, string emailDomain)
        {
            Name = name;
            ContactEmail = contactEmail;
            EmailDomain = emailDomain;

            Teachers = new List<Teacher>();
            Departments = new List<Department>();
            Students = new List<Student>();
        }


        [Required, Key]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required,EmailAddress]
        public string ContactEmail { set; get; }
        [Required]
        public string EmailDomain { set; get; }

        public virtual ICollection<Teacher> Teachers { set; get; }
        public virtual ICollection<Department> Departments { set; get; }
        public ICollection<Student> Students { set; get; }
    }
}
