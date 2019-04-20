using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models.Messenger
{
    public class ChatConnectionMapping 
    {
       
        
        public string Username { set; get; }

        [Key]
        public string ConnectionId { get; set; }

       
    }
}
