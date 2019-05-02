using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    /// <summary>
    ///  This DTO is the object sent from the back end
    ///  After message model object is retrieve from database
    ///  We will use this DTO to map with that object and sent to the frontend for rendering
    /// </summary>
    public class StoredMessageDTO
    {
        public int Id { get; set; }

        // Id of conversation that contain the message
        public int ConversationId { get; set; }

        // Username of user that send the message
        public string SenderUsername { get; set; }

        // Content of the message
        public string MessageContent { get; set; }

        
        public bool OutgoingMessage { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
