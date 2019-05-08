using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.DiscussionForum
{
    public class Answer : IEntity
    {
        public Answer()
        {
            HelpfulCount = 0;
            UnHelpfulCount = 0;
            SpamCount = 0;
            IsCorrectAnswer = false;
            CreatedDate = DateTime.Now;
            UpdatedTime = DateTime.Now;
        }

        public Answer(int questionId, int accountId, string text)
        {
            QuestionId = questionId;
            AccountId = accountId;
            Text = text;
            HelpfulCount = 0;
            UnHelpfulCount = 0;
            SpamCount = 0;
            IsCorrectAnswer = false;
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        //[Key, ForeignKey("Question")]
        [ForeignKey("PostedQuestion")]
        public int QuestionId { get; set; }
        public virtual PostedQuestion PostedQuestion { get; set; }
        //public virtual Question Question { get; set; }


        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        [Required]
        public string Text { get; set; }
        [Required]
        public int HelpfulCount { get; set; }
        [Required]
        public int UnHelpfulCount { get; set; }
        [Required]
        public int SpamCount { get; set; }
        [Required]
        public bool IsCorrectAnswer { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        //Todo delete UpdatedTime
        public DateTime UpdatedTime { get; set; }
    }
}
