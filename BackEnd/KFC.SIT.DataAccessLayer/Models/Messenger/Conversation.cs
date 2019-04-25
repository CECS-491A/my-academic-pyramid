
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Messenger
{
    public class Conversation : IEntity
    {

        public int Id { get; set; }

        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public string MessageContent { get; set; }
        public DateTime CreatedDate { get; set; }


 
 
    }
}
