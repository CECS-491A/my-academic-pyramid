using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class UserSession: IEntity
    {
        public string Token { get; set; }
        public int Id { get; set; }
        public bool IsValid { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public DateTimeOffset RefreshedTime { get; set; }
        public DateTimeOffset ExpirationTime { get; set; }
        [ForeignKey("UserOfSession")]
        public int UserId { get; set; }
        public virtual Account UserOfSession { get; set; }
    }
}
