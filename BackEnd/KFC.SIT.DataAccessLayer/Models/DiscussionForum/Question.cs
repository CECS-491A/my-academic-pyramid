using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.School;


namespace DataAccessLayer.Models.DiscussionForum
{
    public class Question : IEntity 
    {
        //public Question(int schoolId, int departmentId, int courseId, int studentId, string text,
        //    int minimumExpForAnswer, bool isDraft, )
        public Question()
        {
            IsClosed = false;
            SpamCount = 0;
            Answers = new List<Answer>();
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        [ForeignKey("School")]
        public int SchoolId { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string StudentUserName { get; set; }
        // should only be 500 - 2000 chars
        public string Text { get; set; }
        public int MinimumExpForAnswer { get; set; }
        public bool IsDraft { get; set; }
        public bool IsClosed { get; set; }
        public int SpamCount { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public virtual School.School School { set; get; }
        public virtual Department Department { get; set; }
        public virtual Course Course { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
