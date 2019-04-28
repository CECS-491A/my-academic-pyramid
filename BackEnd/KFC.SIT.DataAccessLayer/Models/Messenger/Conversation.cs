
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Messenger
{
    public class Conversation : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        public ICollection<Message> Messages { get; set; }
        public int ContactUserId { get; set; }
        public string ContactUsername { get; set; }
        public bool HasNewMessage { get; set; }
        public DateTime CreatedDate { get; set ; }

        [ForeignKey("UserOfConversation")]
        public int UserId { get; set; }
        public User UserOfConversation { get; set; }









    }
}
