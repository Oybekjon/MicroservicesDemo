using System;
using System.Runtime.Serialization;

namespace MicroservicesDemo.Errors
{
    [Serializable]
    public class NotFoundException : DebugException
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception inner) : base(message, inner) { }
        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}