using DataAccessLayer.Models.School;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Models.DiscussionForum
{
    public class CourseQuestion : PostedQuestion
    {
        public CourseQuestion()
        {
            IsClosed = false;
            SpamCount = 0;
            Answers = new List<Answer>();
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
        }

        //public CourseQuestion(int accountId, int schoolId, int schoolDepartmentId, int courseId,
        //    string text, int expNeededToAnswer, bool isDraft)
        public CourseQuestion(int accountId, int courseId,
            string text, int expNeededToAnswer)
        {
            AccountId = accountId;
            //SchoolId = schoolId;
            //SchoolDepartmentId = schoolDepartmentId;
            //
            //SchoolId = Course.SchoolDepartment.SchoolId;
            //SchoolDepartmentId = Course.SchoolDeparmentId;
            //
            CourseId = courseId;
            Text = text;
            ExpNeededToAnswer = expNeededToAnswer;
            //IsDraft = isDraft;
            IsClosed = false;
            Answers = new List<Answer>();
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
        }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }



        //[ForeignKey("SchoolDepartment")]
        //public int SchoolDepartmentId { get; set; }
        //public virtual SchoolDepartment SchoolDepartment { get; set; }

        //[ForeignKey("School")]
        //public int SchoolId { get; set; }
        //public virtual School.School School { get; set; }
    }
}
