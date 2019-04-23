using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Content { get; set; }
        public int MinimumExpForAnswer { get; set; }
        public bool Draft { get; set; }
        public int Spam { get; set; }
        public String CreatedDate { get; set; }
    }
}
