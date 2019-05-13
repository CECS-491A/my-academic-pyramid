using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.ServiceExceptions
{
    public static class UserManagementExceptions
    {
        public class AccountNotFoundException : Exception
        {
            public AccountNotFoundException()
            {

            }

            public AccountNotFoundException(string message) : base(message)
            {
            }

            public AccountNotFoundException(string message, Exception inner) : base(message, inner)
            {

            }
        }

        public class NotAStudentException : Exception
        {
            public NotAStudentException()
            {

            }

            public NotAStudentException(string message) : base(message)
            {
            }

            public NotAStudentException(string message, Exception inner) : base(message, inner)
            {

            }
        }

        public class DepartmentNotFoundException : Exception
        {
            public DepartmentNotFoundException()
            {

            }

            public DepartmentNotFoundException(string message) : base(message)
            {
            }

            public DepartmentNotFoundException(string message, Exception inner) : base(message, inner)
            {

            }
        }
    }
}