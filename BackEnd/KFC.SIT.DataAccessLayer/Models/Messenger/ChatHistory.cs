
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Messenger
{
    public class ChatHistory : IEntity
    {
        public int Id { get; set; }

        public int ContactId { get; set; }
        public string ContactUsername { get; set; }
        public DateTime ContactTime { get; set ; }


        [ForeignKey("UserOfChatHistory")]
        public int UserId { get; set; }
        public virtual User UserOfChatHistory { get; set; }

       

        

        
    }
}
