using System;
using System.Runtime.Serialization;

namespace MicroservicesDemo.Errors
{
    [Serializable]
    public class PersistenceException : FatalException
    {
        public PersistenceException() { }
        public PersistenceException(String message) : base(message) { }
        public PersistenceException(String message, Exception inner) : base(message, inner) { }
        protected PersistenceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}