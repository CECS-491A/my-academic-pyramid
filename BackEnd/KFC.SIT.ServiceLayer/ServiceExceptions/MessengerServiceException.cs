using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.ServiceExceptions
{
    public class MessengerServiceException :Exception
    {
        public class MessageReceiverNotFoundException : Exception
        {
            public MessageReceiverNotFoundException()
            {

            }

            public MessageReceiverNotFoundException(string message) : base(message)
            {
            }

            public MessageReceiverNotFoundException(string message, Exception inner) : base(message, inner)
            {

            }
        }

        public class DuplicatedFriendException : Exception
        {
            public DuplicatedFriendException()
            {

            }

            public DuplicatedFriendException(string message) : base(message)
            {
            }

            public DuplicatedFriendException(string message, Exception inner) : base(message, inner)
            {

            }
        }

    }
}