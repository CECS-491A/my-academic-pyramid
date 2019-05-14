using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.DiscussionForum;

namespace DataAccessLayer.DTOs
{
    public class AnswerCreateRequestDTO
    {
        public int QuestionId { get; set; }
        //public int AccountId { get; set; }
        public string Text { get; set; }
    }

    public class AnswerResponseDTO
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string Text { get; set; }
        public int HelpfulCount { get; set; }
        public int UnHelpfulCount { get; set; }
        public int SpamCount { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
