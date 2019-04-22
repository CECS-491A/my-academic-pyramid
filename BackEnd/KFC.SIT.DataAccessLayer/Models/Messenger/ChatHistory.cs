
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

        public int ContactId { get; set; }
        public string ContactUsername { get; set; }
        public DateTime ContactTime { get; set ; }

       

        

        
    }
}
