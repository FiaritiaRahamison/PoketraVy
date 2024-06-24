using System;

namespace PoketraVy_backoffice.services
{
    public class LogicException : Exception
    {
        public string redirection {  get; set; }
        public LogicException() { }

        public LogicException(string message)
        : base(message)
        {
        }

        public LogicException(string message,string red)
        : base(message)
        {
            redirection = red;
        }

        // Constructor with error message and inner exception
        public LogicException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
