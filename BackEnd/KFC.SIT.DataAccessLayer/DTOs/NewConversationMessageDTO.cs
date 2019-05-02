using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    /// <summary>
    /// This DTO is the message object send from the front end
    /// This object is used when the front end create a new conversation
    /// </summary>
    public class NewConversationMessageDTO
    {
        // Username of message receiver 
        public string ContactUsername { get; set; }

        // Content of message
        public string MessageContent { get; set; }

    }
}
