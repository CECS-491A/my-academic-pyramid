using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class QuestionDTO
    {
        // need Id for updateQuestion not PostQuestion
        public int Id { get; set; }
        public int PosterId { get; set; }
        public string PosterUserName { get; set; }
        public string Text { get; set; }
        public int MinimumExpForAnswer { get; set; }
        public bool IsDraft { get; set; }
        // not required
        //public bool IsClosed { get; set; }
        //public int SpamCount { get; set; }
        //public String CreatedDate { get; set; }
    }
}
