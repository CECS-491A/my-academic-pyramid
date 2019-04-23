using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.DiscussionForum
{
    public class Question : IEntity//: 
    {
        //public Question()
        //{
        //List<Answer> 
        //}

        //[Key]

        public ICollection<Answer> Answers { get; set; }

        public int Id { get; set; }

        public int AccountId { get; set; }

        // should only be 500 - 2000 chars
        public string Content { get; set; }

        public int MinimumExpForAnswer { get; set; }

        public bool Closed { get; set; }

        public bool Draft { get; set; }

        public int Spam { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
