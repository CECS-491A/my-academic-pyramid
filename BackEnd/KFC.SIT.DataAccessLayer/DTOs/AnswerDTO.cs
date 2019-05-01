using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.DiscussionForum;

namespace DataAccessLayer.DTOs
{
    public class AnswerDTO
    {
        //public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        // public Question Question { get; set; }
        public int QuestionID { get; set; }
        public string Text { get; set; }
        //public int Helpful { get; set; }
        //public int UnHelpful { get; set; }
        //public bool CorrectAnswer { get; set; }
        //public int Spam { get; set; }
        //public DateTime CreatedDate { get; set; }
    }
}
