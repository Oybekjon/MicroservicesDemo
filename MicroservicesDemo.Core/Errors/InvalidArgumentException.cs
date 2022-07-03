using System;
using System.Runtime.Serialization;
namespace MicroservicesDemo.Errors
{
    [Serializable]
    public class InvalidArgumentException : DebugException
    {
        public InvalidArgumentException() { }
        public InvalidArgumentException(string message) : base(message) { }
        public InvalidArgumentException(string message, Exception ex) : base(message, ex) { }
        protected InvalidArgumentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}