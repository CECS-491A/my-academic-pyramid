using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Messenger
{
    public class ChatConnectionMapping 
    {
       
        
        public string Username { set; get; }

        [Key]
        public string ConnectionId { get; set; }

       
    }
}
