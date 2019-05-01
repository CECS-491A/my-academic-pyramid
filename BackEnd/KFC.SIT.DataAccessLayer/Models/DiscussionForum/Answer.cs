﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public Question Question { get; set; }
        public string Text { get; set; }
        public int HelpfulCount { get; set; }
        public int UnHelpfulCount { get; set; }
        public int SpamCount { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
