using System;
using System.Runtime.Serialization;

namespace MicroservicesDemo.Errors
{
    [Serializable]
    public class NoConnectionException : ErrorException
    {
        public NoConnectionException() { }
        public NoConnectionException(String message) : base(message) { }
        public NoConnectionException(String message, Exception inner) : base(message, inner) { }
        protected NoConnectionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}