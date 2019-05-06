using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.DiscussionForum
{
    public class DraftQuestion : Question //IEntity, IQuestion
    {
        public DraftQuestion()
        {
            //QuestionType = "Draft";
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
        }
    }

    //public class DraftQuestion : IEntity, IQuestion
    //{
    //    public DraftQuestion() { }

    //    [Key]
    //    public int Id { get; set; }
    //    public string Text { get; set; }
    //    public int MinimumExpForAnswer { get; set; }
    //    [Required]
    //    public DateTime CreatedDate { get; set; }
    //    public DateTime UpdatedDate { get; set; }

    //    [ForeignKey("Account")]
    //    public int AccountId { get; set; }
    //    public Account Account { get; set; }
    //}
}

