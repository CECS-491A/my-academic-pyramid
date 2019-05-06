using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.DiscussionForum;
using DataAccessLayer.Models.School;


namespace DataAccessLayer.Models.DiscussionForum
{
    public class SchoolQuestion : PostedQuestion , IEntity
    {
        public SchoolQuestion()
        {
            IsClosed = false;
            SpamCount = 0;
            Answers = new List<Answer>();
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
        }

        public SchoolQuestion(int accountId, int schoolId, 
            string text, int expNeededToAnswer)
        {
            AccountId = accountId;
            SchoolId = schoolId;
            
            //
            //SchoolDepartmentId = 
            //CourseId = 
            //
            Text = text;
            ExpNeededToAnswer = expNeededToAnswer;
            //IsDraft = isDraft;
            IsClosed = false;
            SpamCount = 0;
            Answers = new List<Answer>();
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
        }

        //public SchoolQuestion DraftToSchoolQuestion(SchoolQuestion schoolQuestion)
        //{
        //    schoolQuestion.IsDraft = false;
        //    DateCreated = DateTime.Now;
        //    DateUpdated = DateTime.Now;
        //    return schoolQuestion;
        //}

        [ForeignKey("School")]
        public int SchoolId { get; set; }
        public virtual School.School School { get; set; }
    }
}

