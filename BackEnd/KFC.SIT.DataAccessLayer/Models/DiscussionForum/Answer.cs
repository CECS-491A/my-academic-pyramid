using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.DiscussionForum
{
    public class Answer : IEntity
    {
        //public Answer()
        //{

        //}

        //[Key]
        public int Id { get; set; }

        public int AccountId { get; set; }

        public Question Question { get; set; }

        public string Content { get; set; }

        public int Helpful { get; set; }

        public int UnHelpful { get; set; }

        public bool CorrectAnswer { get; set; }

        public int Spam { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
