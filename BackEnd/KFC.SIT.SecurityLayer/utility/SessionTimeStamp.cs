using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLayer.utility
{
    class SessionTimeStamp
    {
        public DateTimeOffset CurrentTime { get; set; }
        public DateTimeOffset ExpireTime { get; set; }
    }
}
