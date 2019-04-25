using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs
{
    public class ContactHistoryDTO
    {
        public int ContactId { get; set; }
        public string ContactUsername { get; set; }
        public DateTime ContactTime { get; set; }
    }
}
