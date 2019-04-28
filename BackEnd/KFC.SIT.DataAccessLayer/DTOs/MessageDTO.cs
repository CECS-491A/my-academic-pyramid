using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class MessageDTO
    {
        //public int AuthUserId { get; set; }
        //
        //public int AuthUsername { get; set; }
        //public int ContactUsername { get; set; }

        public int ConversationId { get; set; }

        public bool OutgoingMessage { get; set; }


        public string MessageContent { get; set; }

        DateTime CreatedDate { get; set; }
    }
}
