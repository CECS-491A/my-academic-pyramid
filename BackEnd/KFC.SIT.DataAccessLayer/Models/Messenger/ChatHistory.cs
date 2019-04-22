using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Messenger
{
    public class ChatHistory : IEntity
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public bool DeleteBySender { get; set; }
        public bool DeleteByReceiver { get; set; }
        public int ReceiverId { get; set; }
        
       
        public DateTime ContactTime { get; set; }

       

        

        
    }
}
