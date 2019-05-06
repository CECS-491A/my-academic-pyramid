using DataAccessLayer.Models.DiscussionForum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.School
{
    public class School : IEntity
    {
        public School()
        {
            Teachers = new List<SchoolTeacher>();
            Departments = new List<SchoolDepartment>();
            //SchoolQuestions = new List<SchoolQuestion>();
            Questions = new List<SchoolQuestion>();
        }

        public School(string name,string contactEmail, string emailDomain)
        {
            Name = name;
            ContactEmail = contactEmail;
            EmailDomain = emailDomain;

            Teachers = new List<SchoolTeacher>();
            Departments = new List<SchoolDepartment>();
            Questions = new List<SchoolQuestion>();
        }


        [Required, Key]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required,EmailAddress]
        public string ContactEmail { set; get; }
        [Required]
        public string EmailDomain { set; get; }

        public virtual ICollection<SchoolTeacher> Teachers { set; get; }
        public virtual ICollection<SchoolDepartment> Departments { set; get; }
        public virtual ICollection<SchoolQuestion> Questions { set; get; }
    }
}
