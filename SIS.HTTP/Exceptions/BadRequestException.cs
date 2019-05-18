using System;

namespace SIS.HTTP.Exceptions
{
    public class BadRequestException : Exception
    {
        private const string BadRequestExceptionDefaultMessage = "The Request was malformed or contains unsupported elements.";

        public BadRequestException(string name)
            : base(name)
        {

        }

        public BadRequestException() : this(BadRequestExceptionDefaultMessage)
        {

        }

    }
}
