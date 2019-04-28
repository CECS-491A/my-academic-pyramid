using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class FriendRelationshipDTO
    {
        public int FriendId { get; set; }
        public string FriendUsername { get; set; }
        public bool IsOnline { get; set; }
    }
}
