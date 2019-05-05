using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.DiscussionForum
{
    public interface IQuestion
    {
        //string Text { get; set; }
        //int MinimumExpForAnswer { get; set; }
        //DateTime CreatedDate { get; set; }
        //DateTime UpdatedDate { get; set; }

        int AccountId { get; set; }
        Account Account { get; set; }
    }
}
