using System;
using System.Runtime.Serialization;

namespace MicroservicesDemo.Errors
{
    [Serializable]
    public class NotAllowedException : DebugException
    {
        public NotAllowedException() { }
        public NotAllowedException(String message) : base(message) { }
        public NotAllowedException(String message, Exception inner) : base(message, inner) { }
        protected NotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}