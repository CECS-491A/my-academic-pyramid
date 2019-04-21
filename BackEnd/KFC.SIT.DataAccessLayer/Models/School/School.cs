using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class School : IEntity
    {
        public School(int id, string name, string contactEmail, string emailDomain)
        {
            Id = id;
            Name = name;
            ContactEmail = contactEmail;
            EmailDomain = emailDomain;

            Teachers = new List<SchoolTeacher>();
            Departments = new List<SchoolDepartment>();
        }


        [Required, Key]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required,EmailAddress]
        public string ContactEmail { set; get; }
        [Required]
        public string EmailDomain { set; get; }

        public ICollection<SchoolTeacher> Teachers { set; get; }
        public ICollection<SchoolDepartment> Departments { set; get; }
    }
}
