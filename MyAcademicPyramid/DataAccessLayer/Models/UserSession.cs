using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repository;

namespace DataAccessLayer
{
    public class UserSession: IEntity
    {
        public string Token { get; set; }
        public int Id { get; set; }
        public bool IsValid { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public DateTimeOffset ExpirationTime { get; set; }
        [ForeignKey("UserOfSession")]
        public int UserId { get; set; }
        public virtual User UserOfSession { get; set; }
    }
}
