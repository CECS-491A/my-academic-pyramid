using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class ConversationDTO
    {
        public string SenderUsername { get; set; }
        public string ReceiverUsername{ get; set; }

        public string MessageContent { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
