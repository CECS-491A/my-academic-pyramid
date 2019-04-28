
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Messenger
{
    public class Message : IEntity
    {
        [Key]
        public int Id { get; set; }


        public bool OutgoingMessage { get; set;  }
        public string MessageContent { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("ConversationOfMessage")]
        public int ConversationId { get; set; }
        public Conversation ConversationOfMessage { get; set; }

       

 
 
    }
}
