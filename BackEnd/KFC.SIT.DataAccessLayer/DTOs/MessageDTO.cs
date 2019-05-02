using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    /// <summary>
    /// This DTO is the message object sent from the front end. 
    /// It is then used to map with the message model in the back end and stored in DB
    /// </summary>
    public class MessageDTO
    {
        public int Id { get; set; }

        // The ID of conversation that contains the message
        public int ConversationId { get; set; }

        // The content of message
        public string MessageContent { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
