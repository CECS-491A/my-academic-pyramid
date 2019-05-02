using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    /// <summary>
    /// This DTO is used to map with backend coversation model  and transfered to the front end
    /// </summary>
    public class ConversationDTO
    {
        public int Id { get; set; }

        // Contact username will be looked up using contact id in conversation model
        public string ContactUsername{ get; set; }

        // Used to mark if the conversation receive a new message
        public bool HasNewMessage { get; set; }
        public string CreatedDate { get; set; }
    }
}
