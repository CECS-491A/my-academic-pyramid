using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    /// <summary>
    /// DTO is used to map with FriendRelationship model to be transfered to the front end 
    /// </summary>
    public class FriendRelationshipDTO
    {
        public int FriendId { get; set; }
        public string FriendUsername { get; set; }
        public bool IsOnline { get; set; }
    }
}
