using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Messenger
{
    public class Conversation : IEntity
    {

        public int Id { get; set; }

        public String SenderUserName { get; set; } 
        public String ReceiverUserName { get; set; }
        public string MessageContent { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
