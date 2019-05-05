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
    public class DepartmentQuestion : PostedQuestion
    {
        public DepartmentQuestion()
        {
            IsClosed = false;
            SpamCount = 0;
            Answers = new List<Answer>();
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
        }

        public DepartmentQuestion(int accountId, int schoolDepartmentId, 
            string text, int expNeededToAnswer)
        {
            AccountId = accountId;
            //
            //SchoolId = SchoolDepartment.SchoolId;
            //CourseId = null;
            //
            SchoolDepartmentId = schoolDepartmentId;
            Text = text;
            ExpNeededToAnswer = expNeededToAnswer;
            //IsDraft = isDraft;
            IsClosed = false;
            Answers = new List<Answer>();
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
        }

        [ForeignKey("SchoolDepartment")]
        public int SchoolDepartmentId { get; set; }
        public virtual SchoolDepartment SchoolDepartment { get; set; }


        //[ForeignKey("School")]
        //public int SchoolId { get; set; }
        //public virtual School.School School { get; set; }
    }
}
