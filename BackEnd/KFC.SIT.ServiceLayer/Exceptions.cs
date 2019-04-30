using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer
{
    // Examples

    //public class InvalidStringException : Exception
    //{
    //    public InvalidStringException() { }

    //    public InvalidStringException(string message) : base(message) { }
    //}

    //public class InvalidEmailException : Exception
    //{
    //    public InvalidEmailException() { }

    //    public InvalidEmailException(string message) : base(message) { }
    //}

    public class InvalidQuestionLengthException : Exception
    {
        public InvalidQuestionLengthException() { }

        public InvalidQuestionLengthException(string message) : base(message) { }
    }

    public class QuestionIsClosedException : Exception
    { 
        public QuestionIsClosedException() { }

        public QuestionIsClosedException(string message) : base(message) { }
    }

    public class QuestionUnavailableException : Exception
    {
        public QuestionUnavailableException() { }

        public QuestionUnavailableException(string message) : base(message) { }
    }

    public class NotEnoughExpException : Exception
    {
        public NotEnoughExpException() { }

        public NotEnoughExpException(string message) : base(message) { }
    }

    public class InvalidUserException : Exception
    {
        public InvalidUserException() { }

        public InvalidUserException(string message) : base(message) { }
    }
}