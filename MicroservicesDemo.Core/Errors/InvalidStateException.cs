using System;
using System.Runtime.Serialization;
namespace MicroservicesDemo.Errors
{
    [Serializable]
    public class InvalidStateException : ErrorException
    {
        public InvalidStateException() { }
        public InvalidStateException(string message) : base(message) { }
        public InvalidStateException(string message, Exception inner) : base(message, inner) { }
        protected InvalidStateException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}