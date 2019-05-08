using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.ServiceExceptions 
{
    public class SchoolRegistrationException : Exception
    {
        public class SchoolDuplicationException: Exception
        {
            public SchoolDuplicationException()
            {

            }
            public SchoolDuplicationException(string message) : base(message)
            {

            }
            public SchoolDuplicationException(string message, Exception inner) : base(message, inner)
            {

            }
        }
       
    }
}