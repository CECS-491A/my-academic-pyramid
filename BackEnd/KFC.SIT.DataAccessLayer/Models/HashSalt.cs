using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class HashSalt
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
