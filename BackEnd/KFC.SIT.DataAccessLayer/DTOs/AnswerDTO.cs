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
        public int Id { get; set; }
        public int AccountId { get; set; }
        public Question Question { get; set; }
        public string Content { get; set; }
        public int Helpful { get; set; }
        public int UnHelpful { get; set; }
        public bool CorrectAnswer { get; set; }
        public int Spam { get; set; }
        public String CreatedDate { get; set; }
    }
}
