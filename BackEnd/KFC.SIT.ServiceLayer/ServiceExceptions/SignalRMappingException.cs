using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.ServiceExceptions
{
    public class SignalRException
    {
        public class SignalRMappingException : Exception
        {
            public SignalRMappingException()
            {
            }

            public SignalRMappingException(string message) : base(message)
            {
            }

            public SignalRMappingException(string message, Exception inner) : base (message, inner)
            {

            }
        }


    }
}