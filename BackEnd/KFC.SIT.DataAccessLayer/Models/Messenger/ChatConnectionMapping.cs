using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Messenger
{
    public class ChatConnectionMapping 
    {
       
        
        public int UserId { set; get; }

        [Key]
        public string ConnectionId { get; set; }

       
    }
}
