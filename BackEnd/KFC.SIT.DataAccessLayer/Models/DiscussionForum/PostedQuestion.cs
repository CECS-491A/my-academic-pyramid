
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.DiscussionForum
{
    public abstract class PostedQuestion : Question
    {
        public PostedQuestion()
        {
            ////IsDraft = false;
            //IsClosed = false;
            //SpamCount = 0;
            //CreatedDate = DateTime.Now;
            //DateUpdated = DateTime.Now;
            //Answers = new List<Answer>();
        }
        [Required]
        public bool IsClosed { get; set; }
        [Required]
        public int SpamCount { get; set; }
        public ICollection<Answer> Answers { get; set; }

        //public Nullable<int> SchoolId { get; set; }
        //public Nullable<int> SchoolDepartmentId { get; set; }
        //public Nullable<int> CourseId { get; set; }

        //public PostedQuestion()
        //{
        //    //IsDraft = false;
        //    IsClosed = false;
        //    SpamCount = 0;
        //    CreatedDate = DateTime.Now;
        //    DateUpdated = DateTime.Now;
        //    Answers = new List<Answer>();
        //}

        ////public Question(bool isDraft=true)
        ////{
        ////    IsDraft = isDraft;
        ////}

        ////[Key]
        ////public int Id { get; set; }

        ////public string Text { get; set; }
        ////public int MinimumExpForAnswer { get; set; }

        ////public bool IsDraft { get; set; }
        //[Required]
        //public new int MinimumExpForAnswer;
        //public bool IsClosed { get; set; }
        //public int SpamCount { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public ICollection<Answer> Answers { get; set; }


        //[Key, ForeignKey("Account")]
        //public int AccountId { get; set; }
        //public Account Account { get; set; }

        //public Question DraftToQuestion(QuestionDraft draft)
        //{
        //    //Question question = new Question();
        //    //draft.IsDraft = false;
        //    draft.IsClosed = false;
        //    draft.SpamCount = 0;
        //    draft.Answers = new List<Answer>();
        //    draft.CreatedDate = DateTime.Now;
        //    return draft;
        //}

    }
}
