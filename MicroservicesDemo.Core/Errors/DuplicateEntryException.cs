using System;
using System.Runtime.Serialization;

namespace MicroservicesDemo.Errors
{
    [Serializable]
    public class DuplicateEntryException : DebugException
    {
        public DuplicateEntryException() { }
        public DuplicateEntryException(String message) : base(message) { }
        public DuplicateEntryException(String message, Exception inner) : base(message, inner) { }
        protected DuplicateEntryException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}