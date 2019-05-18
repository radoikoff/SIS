using System;

namespace SIS.HTTP.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        private const string InternalServerErrorExceptionDefaultMessage = "The Server has encountered an error.";

        public InternalServerErrorException(string name)
            : base(name)
        {

        }

        public InternalServerErrorException()
            : this(InternalServerErrorExceptionDefaultMessage)
        {

        }

    }
}
