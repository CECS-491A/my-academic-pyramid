using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class UserSession
    {
        public string Token { get; set; }
        public Guid SessionID { get; set; }
        public Guid UserID { get; set; }
        public bool IsValid { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
