using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models.Messenger
{
    public class FriendRelationship : IEntity
    {
        [Key]
        public int Id { get; set; }

        
        public int  FriendId { get; set; }
 
      
        //public bool isOnline { get;set; }
        [ForeignKey("UserOfRelationship")]
        public int UserId { get; set; }
        public virtual Account UserOfRelationship { get; set; }

       
    }
}
