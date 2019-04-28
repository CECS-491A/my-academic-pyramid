using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class StoredMessageDTO
    {
        public int ConversationId { get; set; }
        public string SenderUsername { get; set; }
        public string MessageContent { get; set; }
        DateTime CreatedDate { get; set; }
    }
}
