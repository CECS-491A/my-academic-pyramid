using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.DiscussionForum
{
    public abstract class Question : IEntity//, IQuestion
    {
        public Question() { }

        [Key]
        public int Id { get; set; }
        //public string QuestionType { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public int ExpNeededToAnswer { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        //[Required]
        //public bool IsClosed { get; set; }
        //[Required]
        //public bool IsDraft { get; set; }
        //[Required]
        //public int SpamCount { get; set; }
        //public ICollection<Answer> Answers { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
